using System.IdentityModel.Tokens.Jwt;
using RedPoint.Chat.Data;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;

namespace RedPoint.Chat.Services
{
    /// <summary>
    /// Provides methods necessary for real-time chat functionality
    /// </summary>
    public interface IChatHubService
    {

        /// <summary>
        /// Creates a server from provided <c>ServerIconDto</c> and adds it to the server repository. 
        /// </summary>
        /// <param name="serverInfo"></param>
        void AddServer(ServerInfoDto serverInfo, IChatEntityRepositoryProxy<Server, ChatDbContext> repo);
        
        /// <summary>
        /// Creates a message from provided <c>MessageDto</c> and adds it to the message repository.
        /// </summary>
        /// <param name="message"></param>
        void AddMessage(MessageDto message,
            IChatEntityRepositoryProxy<Message, ChatDbContext> messageRepo,
            IChatEntityRepositoryProxy<Channel, ChatDbContext> channelRepo);

        /// <summary>
        /// Creates a channel from provided <c>ChannelIconDto</c> and adds it to the channel repository. 
        /// </summary>
        /// <param name="channelInfo"></param>
        void AddChannel(ChannelInfoDto channelInfo,
            IChatEntityRepositoryProxy<Channel, ChatDbContext> channelRepo,
            IChatEntityRepositoryProxy<Server, ChatDbContext> serverRepo);

        
        /// <summary>
        /// Deletes the channel from channel repository.
        /// </summary>
        /// <param name="channelId">ID of the deleted channel.</param>
        void DeleteChannel(int channelId, IChatEntityRepositoryProxy<Channel, ChatDbContext> repo);
        
        /// <summary>
        /// Deletes the server from server repository.
        /// </summary>
        /// <param name="serverId">ID of the deleted server.</param>
        void DeleteServer(int serverId, IChatEntityRepositoryProxy<Server, ChatDbContext> repo);
        
        
        /// <summary>
        /// Deletes the message from message repository.
        /// </summary>
        /// <param name="messageId">ID of the deleted message.</param>
        void DeleteMessage(int messageId, IChatEntityRepositoryProxy<Message, ChatDbContext> repo);
    }
}