using RedPoint.Chat.Models.Dto;

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
        /// <param name="serverIcon"></param>
        void AddServer(ServerIconDto serverIcon);
        
        /// <summary>
        /// Creates a message from provided <c>MessageDto</c> and adds it to the message repository.
        /// </summary>
        /// <param name="channelId">ID of the channel that will contain the added message</param>
        /// <param name="serverId">ID of the server containing the channel</param>
        /// <param name="message"></param>
        void AddMessage(int channelId, int serverId, MessageDto message);
        
        /// <summary>
        /// Creates a channel from provided <c>ChannelIconDto</c> and adds it to the channel repository. 
        /// </summary>
        /// <param name="serverId">ID of the server that will contain the added channel</param>
        /// <param name="channelIcon"></param>
        void AddChannel(int serverId, ChannelIconDto channelIcon);

        
        /// <summary>
        /// Deletes the channel from channel repository.
        /// </summary>
        /// <param name="channelId">ID of the deleted channel.</param>
        /// <param name="serverId">ID of the server containing the channel.</param>
        void DeleteChannel(int channelId, int serverId);
        
        /// <summary>
        /// Deletes the server from server repository.
        /// </summary>
        /// <param name="serverId">ID of the deleted server.</param>
        void DeleteServer(int serverId);
        
        /// <summary>
        /// Deletes the message from message repository.
        /// </summary>
        /// <param name="messageId">ID of the deleted message.</param>
        /// <param name="channelId">ID of the channel containing the message.</param>
        /// <param name="serverId">ID of the server containing the channel.</param>
        void DeleteMessage(int messageId, int channelId, int serverId);
    }
}