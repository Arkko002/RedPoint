using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Chat.Data;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services;

namespace RedPoint.Chat.Hubs
{
    /// <summary>
    /// Defines necessary methods for real-time chat functionality.
    /// </summary>
    public interface IChatHub
    {
        Task ServerAdded(ServerIconDto serverIcon);
        Task MessageAdded(MessageDto message);
        Task ChannelAdded(ChannelIconDto channelIcon);

        Task ServerDeleted(int serverId);
        Task MessageDeleted(int messageId);
        Task ChannelDeleted(int channelId);

        Task ChannelChanged(string channelId);
        Task ServerChanged(string serverId);

        Task JoinedServer(ServerIconDto serverIcon);
    }

    //TODO Rework this with UniqueIDs generated with hashing
    /// <summary>
    /// Implements real-time chat functionality.
    /// </summary>
    public class ChatHub : Hub<IChatHub>
    {
        private readonly IChatHubService _chatService;

        private readonly ChatEntityRepositoryProxy<Message, ChatDbContext> _messageRepo;
        private readonly ChatEntityRepositoryProxy<Channel, ChatDbContext> _channelRepo;
        private readonly ChatEntityRepositoryProxy<Server, ChatDbContext> _serverRepo;

        public ChatHub(IChatHubService chatService,
            ChatEntityRepositoryProxy<Message, ChatDbContext> messageRepo,
            ChatEntityRepositoryProxy<Channel, ChatDbContext> channelRepo,
            ChatEntityRepositoryProxy<Server, ChatDbContext> serverRepo,
            ChatEntityRepositoryProxy<ChatUser, ChatDbContext> userRepo)
        {
            _chatService = chatService;
            _messageRepo = messageRepo;
            _channelRepo = channelRepo;
            _serverRepo = serverRepo;
            
            _chatService.AssignChatUserFromToken((JwtSecurityToken)Context.Items["UserToken"], userRepo);
        }

        public Task AddServer(ServerIconDto serverIcon)
        {
            _chatService.AddServer(serverIcon, _serverRepo);

            Groups.AddToGroupAsync(Context.ConnectionId, serverIcon.HubGroupId.IdString);
            return Clients.Group(serverIcon.HubGroupId.IdString).ServerAdded(serverIcon);
        }

        public Task AddMessage(MessageDto message, string channelUniqueId)
        {
            _chatService.AddMessage(message, _messageRepo, _channelRepo);

            return Clients.Group(channelUniqueId).MessageAdded(message);
        }

        public Task AddChannel(ChannelIconDto channelIcon)
        {
            _chatService.AddChannel(channelIcon, _channelRepo, _serverRepo);

            return Clients.Group(serverUniqueId).ChannelAdded(channelIcon);
        }

        public Task DeleteServer(int serverId, string serverGroupId)
        {
            _chatService.DeleteServer(serverId, _serverRepo);

            return Clients.Group(serverGroupId).ServerDeleted(serverId);
        }

        public Task DeleteMessage(int messageId, string channelGroupId)
        {
            _chatService.DeleteMessage(messageId, _messageRepo);

            return Clients.Group(channelGroupId).MessageDeleted(messageId);
        }

        public Task DeleteChannel(int channelId, string serverGroupId)
        {
            _chatService.DeleteChannel(channelId, _channelRepo);

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