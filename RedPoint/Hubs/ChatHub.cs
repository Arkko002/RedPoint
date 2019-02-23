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
using RedPoint.Infrastructure.Facades;

namespace RedPoint.Hubs
{
    #if DEBUG
    #else
        [Authorize] 
    #endif
    public class ChatHub : Hub<IChatHub>
    {
        private ChatFacade _chat;
        private ApplicationDbContext _db;

        public ChatHub(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _chat = new ChatFacade(db, userManager);
        }

        /// <summary>
        /// Calls ChatFacade.CreateMessage and sends the created message to clients in channel group
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="channelId"></param>
        public async Task Send(string msg, string channelId)
        {
            var message = _chat.CreateMessage(Context.UserIdentifier, msg, channelId).Result;
            if (!(message is null))
            {
                await Clients.Group(channelId).AddNewMessage(message);
            }        
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
        public async Task ChannelChanged(string channelId)
        {
            var channel = _chat.CheckChannelChange(out var user, Context.UserIdentifier, channelId);
            if (!(channel is null))
            {
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

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.CurrentChannelId.ToString());
                user.CurrentChannelId = channelId;
                await Groups.AddToGroupAsync(Context.ConnectionId, channelId);
                await _db.SaveChangesAsync();

                return;                
            }

            await Clients.Caller.ChannelDoesntExist();
        }

        public async Task ServerChanged(int serverId)
        {
            var server = _chat.CheckServerChange(Context.UserIdentifier, serverId);
            if (!(server is null))
            {
                var channels = server.Channels.ToList();
                await Clients.Caller.GetChannnelList(channels);
            }

            await Clients.Caller.ServerDoesntExist();

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