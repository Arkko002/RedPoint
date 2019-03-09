using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using RedPoint.Data;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
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
            var message = await _chat.CreateMessage(Context.UserIdentifier, msg, channelId);
            if (!(message is null))
            {
                await Clients.Group(channelId).AddNewMessage(message);
            }        
        }

        /// <summary>
        /// Gets last 50 Messages from Channel and sends them to Caller
        /// </summary>
        /// <param name="channelId"></param>
        public async Task ChannelChanged(string channelId)
        {
            var resultTuple = await _chat.CheckChannelChange(Context.UserIdentifier, channelId);
            if (resultTuple is null)
            {
                await Clients.Caller.ChannelDoesntExist();
                return;
            }

            if (resultTuple.Value.canView)
            {
                var lastMsgs = resultTuple.Value.channel.Messages.Skip(Math.Max(0, resultTuple.Value.channel.Messages.Count() - 50));

                await Clients.Caller.GetMessagesFromDb(lastMsgs);
            }

            if (!resultTuple.Value.canWrite)
            {
                await Clients.Caller.UserCantWrite();
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, resultTuple.Value.user.CurrentChannelId);
            resultTuple.Value.user.CurrentChannelId = channelId;
            await Groups.AddToGroupAsync(Context.ConnectionId, channelId);
            await _db.SaveChangesAsync();
        }

        public async Task ServerChanged(int serverId)
        {
            var server = await _chat.CheckServerChange(Context.UserIdentifier, serverId);
            if (server is null)
            {
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
        Task UserCantWrite();
        Task UserNotLoggedIn(string v);
    }
}