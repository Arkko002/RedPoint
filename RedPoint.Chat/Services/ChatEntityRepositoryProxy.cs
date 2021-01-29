using RedPoint.Chat.Data;
using RedPoint.Chat.Exceptions;
using RedPoint.Chat.Models.Chat;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Services
{

    //TODO Improper implementation of proxy pattern
    /// <summary>
    /// Proxy for <c>EntityRepository</c> objects that provides error handling for repository operations.
    /// </summary>
    public class ChatEntityRepositoryProxy : IChatEntityRepositoryProxy
    {
        public EntityRepository<Channel, ChatDbContext> ChannelRepository { get; }
        public EntityRepository<Message, ChatDbContext> MessageRepository { get; }
        public EntityRepository<Server, ChatDbContext> ServerRepository { get; }
        public ChatEntityRepositoryProxy(EntityRepository<Server, ChatDbContext> serverRepo,
            EntityRepository<Channel, ChatDbContext> channelRepo,
            EntityRepository<Message, ChatDbContext> messageRepo)
        {
            ChannelRepository = channelRepo;
            MessageRepository = messageRepo;
            ServerRepository = serverRepo;
        }
        
        /// <summary>
        /// Returns a server on valid request.
        /// Passes a ServerNotFound error to error handler if server is not in the repository.
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="requestingUser"></param>
        public Server TryFindingServer(int serverId, ChatUser requestingUser)
        {
            var server = ServerRepository.Find(serverId);

            if (server == null)
            {
                throw new EntityNotFoundException("Requested server was not found.", serverId);
            }

            return server;
        }

        /// <summary>
        /// Returns a channel on valid request.
        /// Passes a ChannelNotFound error to error handler if channel is not in the repository.
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="requestingUser"></param>
        public Channel TryFindingChannel(int channelId, ChatUser requestingUser)
        {
            var channel = ChannelRepository.Find(channelId);

            if (channel == null)
            {
                throw new EntityNotFoundException("Requested channel was not found.", channelId);
            }

            return channel;
        }

        /// <summary>
        /// Returns a message on valid request.
        /// Passes a MessageNotFound error to error handler if message is not in the repository.
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="requestingUser"></param>
        public Message TryFindingMessage(int messageId, ChatUser requestingUser)
        {
            var message = MessageRepository.Find(messageId);

            if (message == null)
            {
                throw new EntityNotFoundException("Requested message was not found.", messageId);
            }

            return message;
        }
    }
}