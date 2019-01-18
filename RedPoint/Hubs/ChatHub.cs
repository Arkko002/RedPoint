using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using RedPoint.Data;
using RedPoint.Infrastructure;
using RedPoint.Models.Users_Permissions_Models;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RedPoint.Hubs
{
    #if DEBUG
    #else
        [Authorize] 
    #endif
    public class ChatHub : Hub<IChatHub>
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        public ChatHub(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        /// <summary>
        /// Server method called to add Message to database
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="channelId"></param>
        public async Task Send(string msg, int channelId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            var channel = _db.Channels.Find(channelId);
            if (channel is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to write in nonexistent channel (Channel ID: {2))", user.UserName, user.Id, channelId);
                await Clients.Caller.ChannelDoesntExist();
                return;
            }

            PermissionsManager permissionsManager = new PermissionsManager();
            if (!permissionsManager.CheckUserChannelPermissions(user, channel, new[] { PermissionTypes.CanWrite }))
            {
                _logger.Warn("{0} (ID: {1}) tried to write in channel without write permission (Channel ID: {2))", user.UserName, user.Id, channelId);
                await Clients.Caller.UserCantWrite();
                return;
            }

            if (user.UserStub is null)
            {
                var stubManager = new UserStubManager(_db);
                user.UserStub = stubManager.CreateUserStub(user);
            }

            Message message = new Message()
            {
                UserStub = user.UserStub,
                Text = msg,
                DateTimePosted = DateTime.Now
            };

            channel.Messages.Add(message);
            _db.SaveChanges();

            await Clients.Group(channelId.ToString()).AddNewMessage(message);
        }

        /// <summary>
        /// Searches for given Message.Text in database
        /// </summary>
        /// <param name="text"></param>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        public async Task Search(string text, ChannelStub channel, UserStub user)
        {
            //Message[] msgArr = _db.Messages.Where(m => (m.Text == text &&
            //                                            m.ChannelStub == channel &&
            //                                            m.UserStub == user)).ToArray();

            IQueryable<Message> q = _db.Messages;

            if (!(text is null))
            {
                q = q.Where(m => m.Text == text);
            }
            if ((channel is null))
            {
                q = q.Where(m => m.ChannelStub == channel);
            }

            if (!(user is null))
            {
                q = q.Where(m => m.UserStub == user);
            }

            var msgArr = q.ToArray();

            await Clients.Caller.ShowSearchResult(msgArr);
        }

        /// <summary>
        /// Returns UserStub list with UserStub.AppUserName containing given parameter
        /// </summary>
        /// <param name="nick"></param>
        public async Task UserAutocomplete(string nick)
        {
            var userList = _db.UserStubs.Where(u => u.AppUserName.StartsWith(nick)).ToList();

            await Clients.Caller.ShowUserAutocomplete(userList);
        }

        /// <summary>
        /// Gets last 50 Messages from Channel and sends them to Caller
        /// </summary>
        /// <param name="channelId"></param>
        public async Task ChannelChanged(int channelId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            var channel = _db.Channels.Find(channelId);
            if (channel is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to join nonexistent channel (Channel ID: {2))", user.UserName, user.Id, channelId);
                await Clients.Caller.ChannelDoesntExist();
                return;
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.CurrentChannelId.ToString());
            user.CurrentChannelId = channelId;
            await Groups.AddToGroupAsync(Context.ConnectionId, channelId.ToString());
            _db.SaveChanges();

            PermissionsManager permissionsManager = new PermissionsManager();
            if (permissionsManager.CheckUserChannelPermissions(user, channel, new[] { PermissionTypes.CanView }))
            {
                var lastMsgs = channel.Messages.Skip(Math.Max(0, channel.Messages.Count() - 50));

                await Clients.Caller.GetMessagesFromDb(lastMsgs);
            }

            if (!permissionsManager.CheckUserChannelPermissions(user, channel, new[] { PermissionTypes.CanWrite }))
            {
                await Clients.Caller.UserCantWrite();
            }
        }

        public async Task ServerChanged(int serverId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            var server = _db.Servers.Find(serverId);
            if (server is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to join nonexistent server (Server ID: {2))", user.UserName, user.Id, serverId);
                await Clients.Caller.ServerDoesntExist();
                return;
            }

            var channels = server.Channels.ToList();

            await Clients.Caller.GetChannnelList(channels);
        }
    }

    public interface IChatHub
    {
        Task AddNewMessage(Message message);
        Task ChannelDoesntExist();
        Task GetChannnelList(List<Channel> channels);
        Task GetMessagesFromDb(IEnumerable<Message> lastMsgs);
        Task GetServerList(List<Server> list);
        Task ServerDoesntExist();
        Task ShowSearchResult(Message[] msgList);
        Task ShowUserAutocomplete(List<UserStub> userList);
        Task UserCantWrite();
        Task UserNotLoggedIn(string v);
    }
}