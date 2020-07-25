using Microsoft.Extensions.Logging;
using RedPoint.Areas.Account.Models;
using RedPoint.Chat.Models;
using RedPoint.Chat.Services.Security;
using RedPoint.Data;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Services
{
    public class ChatEntityRepositoryProxy : IChatEntityRepositoryProxy
    {
        private readonly IChatErrorHandler _errorHandler;

        public ChatEntityRepositoryProxy(EntityRepository<Server, ApplicationDbContext> serverRepo,
            EntityRepository<Channel, ApplicationDbContext> channelRepo,
            EntityRepository<Message, ApplicationDbContext> messageRepo,
            IChatErrorHandler errorHandler)
        {
            ChannelRepository = channelRepo;
            MessageRepository = messageRepo;
            ServerRepository = serverRepo;
            _errorHandler = errorHandler;
        }

        public EntityRepository<Channel, ApplicationDbContext> ChannelRepository { get; }
        public EntityRepository<Message, ApplicationDbContext> MessageRepository { get; }
        public EntityRepository<Server, ApplicationDbContext> ServerRepository { get; }

        public Server TryFindingServer(int serverId, ChatUser requestingUser)
        {
            var server = ServerRepository.Find(serverId);

            if (server == null)
            {
                var chatError = new ChatError(ChatErrorType.ServerNotFound,
                    requestingUser,
                    LogLevel.Warning,
                    $"Non-existing server (ID: {serverId}) requested by {requestingUser.Id}");

                _errorHandler.HandleChatError(chatError);
            }

            return server;
        }

        public Channel TryFindingChannel(int channelId, ChatUser requestingUser)
        {
            var channel = ChannelRepository.Find(channelId);

            if (channel == null)
            {
                var chatError = new ChatError(ChatErrorType.ChannelNotFound,
                    requestingUser,
                    LogLevel.Warning,
                    $"Non-existing channel (ID: {channelId}) requested by {requestingUser.Id}");

                _errorHandler.HandleChatError(chatError);
            }

            return channel;
        }

        public Message TryFindingMessage(int messageId, ChatUser requestingUser)
        {
            var message = MessageRepository.Find(messageId);

            if (message == null)
            {
                var chatError = new ChatError(ChatErrorType.ChannelNotFound,
                    requestingUser,
                    LogLevel.Warning,
                    $"Non-existing message (ID: {messageId}) requested by {requestingUser.Id}");

                _errorHandler.HandleChatError(chatError);
            }

            return message;
        }
    }
}