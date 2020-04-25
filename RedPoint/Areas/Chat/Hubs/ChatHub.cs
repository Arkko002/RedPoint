using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services;

namespace RedPoint.Areas.Chat.Hubs
{
    public interface IChatHub
    {
        Task ServerAdded(ServerIconDto serverIcon);
        Task MessageAdded(MessageDto message);
        Task ChannelAdded(ChannelIconDto channelIcon);

        Task ServerDeleted(int serverId);
        Task MessageDeleted(int messageId);
        Task ChannelDeleted(int channelId);

        Task ChannelChanged(string channelUniqueId);
        Task ServerChanged(string serverUniqueId);

        Task JoinedServer(ServerIconDto serverIcon);
    }

    public class ChatHub : Hub<IChatHub>
    {
        private readonly IChatHubService _chatService;

        public ChatHub(IChatHubService chatService)
        {
            _chatService = chatService;
            _chatService.AssignApplicationUser(Context.User);
        }

        public Task AddServer(ServerIconDto serverIcon)
        {
            _chatService.AddServer(serverIcon);

            Groups.AddToGroupAsync(Context.ConnectionId, serverIcon.HubGroupId.IdString);
            return Clients.Group(serverIcon.HubGroupId.IdString).ServerAdded(serverIcon);
        }

        public Task AddMessage(MessageDto message, string channelUniqueId, int channelId, int serverId)
        {
            _chatService.AddMessage(channelId, serverId, message);

            return Clients.Group(channelUniqueId).MessageAdded(message);
        }

        public Task AddChannel(ChannelIconDto channelIcon, string serverUniqueId, int serverId)
        {
            _chatService.AddChannel(serverId, channelIcon);

            return Clients.Group(serverUniqueId).ChannelAdded(channelIcon);
        }

        public Task DeleteServer(int serverId, string serverGroupId)
        {
            _chatService.DeleteServer(serverId);

            return Clients.Group(serverGroupId).ServerDeleted(serverId);
        }

        public Task DeleteMessage(int messageId, int channelId, int serverId, string channelGroupId)
        {
            _chatService.DeleteMessage(messageId, channelId, serverId);

            return Clients.Group(channelGroupId).MessageDeleted(messageId);
        }

        public Task DeleteChannel(int channelId, int serverId, string serverGroupId)
        {
            _chatService.DeleteChannel(channelId, serverId);

            return Clients.Group(serverGroupId).ChannelDeleted(channelId);
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

        public Task JoinServer(ServerIconDto serverIcon)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, serverIcon.HubGroupId.IdString);

            return Clients.Caller.JoinedServer(serverIcon);
        }
    }
}