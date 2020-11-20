using System.Collections.Generic;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;

namespace RedPoint.Chat.Services
{
    /// <summary>
    /// Provides methods necessary for non real-time chat functionality
    /// </summary>
    public interface IChatControllerService
    {
        /// <summary>
        /// Gets a list of server DTOs stripped of internal data that current user is part of.
        /// </summary>
        /// <param name="dtoFactory">Factory handling creation of <c>ServerIconDto</c></param>
        List<ServerIconDto> GetUserServers(IChatDtoFactory<Server, ServerIconDto> dtoFactory);
        
        /// <summary>
        /// Gets internal data of the server with provided ID.
        /// </summary>
        /// <param name="serverId">ID of the requested server.</param>
        /// <param name="dtoFactory">Factory handling creation of <c>ServerDataDto</c></param>
        ServerDataDto GetServerData(int serverId, IChatDtoFactory<Server, ServerDataDto> dtoFactory);

        /// <summary>
        /// Gets a list of message DTOs from a channel with provided ID.
        /// </summary>
        /// <param name="channelId">ID of the channel containing messages.</param>
        /// <param name="serverId">ID of the server containing the channel.</param>
        /// <param name="dtoFactory">Factory handling creation of <c>MessageDto</c></param>
        List<MessageDto> GetChannelMessages(int channelId, int serverId,
            IChatDtoFactory<Message, MessageDto> dtoFactory);
    }
}