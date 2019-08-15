using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Chat.Services.Facades;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Hubs
{
    /// <summary>
    /// Central hub of the application, provides communication with the basic chat functionality.
    /// </summary>
    public class ChatHub : Hub<IChatHub>
    {
        private readonly ChatFacade _chat;
        private readonly ApplicationDbContext _db;

        public ChatHub(ApplicationDbContext db, UserManager<ApplicationUser> userManager, HubUserInputValidator inputValidator)
        {
            _db = db;
            _chat = new ChatFacade(db, userManager, inputValidator);
        }

        /// <summary>
        /// Delegates the creation of message, returns the created message to all client's in the chat room on success.
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
        /// Called when user changes the Channel. Gets last 50 Messages from Channel and sends them to Caller on success. 
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
                var lastMsgs = resultTuple.Value.channel.Messages.Skip(Math.Max(0, resultTuple.Value.channel.Messages.Count - 50));

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

        /// <summary>
        /// Called when user changes the Server. Returns the list of Server's Channels to caller on success. 
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
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