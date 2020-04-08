using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services;
using RedPoint.Services;

namespace RedPoint.Areas.Chat.Hubs
{
    public interface IChatHub
    {
        Task ServerAdded(ServerDto server);
        Task MessageAdded(MessageDto message);
        Task ChannelAdded(ChannelDto channel);

        Task ChannelChanged(string channelUniqueId);
        Task ServerChanged(string serverUniqueId);

        Task JoinedServer(ServerDto server);
    }

    public class ChatHub : Hub<IChatHub>
    {
        private IChatHubService _chatService;

        public ChatHub(IChatHubService chatService)
        {
            _chatService = chatService;
        }

        public Task AddServer(ServerDto server)
        {
            _chatService.TryAddingServer(server);

            Groups.AddToGroupAsync(Context.ConnectionId, server.UniqueId.IdString);
            return Clients.Group(server.UniqueId.IdString).ServerAdded(server);
        }

        public Task AddMessage(MessageDto message, string channelUniqueId)
        {
            _chatService.TryAddingMessage(channelUniqueId, message);

            return Clients.Group(channelUniqueId).MessageAdded(message);
        }

        public Task AddChannel(ChannelDto channel, string serverUniqueId)
        {
            _chatService.TryAddingChannel(serverUniqueId, channel);

            return Clients.Group(serverUniqueId).ChannelAdded(channel);
        }

        public Task ChangeChannel(string prevChannelUniqueId, string newChannelUniqueId)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, prevChannelUniqueId);
            Groups.AddToGroupAsync(Context.ConnectionId, newChannelUniqueId);

            return Clients.Caller.ChannelChanged(newChannelUniqueId);
        }

        public Task ChangeServer(string prevServerUniqueId, string newServerUniqueId)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, prevServerUniqueId);
            Groups.AddToGroupAsync(Context.ConnectionId, newServerUniqueId);

            return Clients.Caller.ServerChanged(newServerUniqueId);
        }

        public Task JoinServer(ServerDto server)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, server.UniqueId.IdString);

            return Clients.Caller.JoinedServer(server);
        }
    }
}