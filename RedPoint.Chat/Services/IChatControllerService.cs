using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RedPoint.Chat.Data;
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
        CurrentUserDto GetCurrentUser(IChatDtoFactory<ChatUser, CurrentUserDto> dtoFactory);
        
        UserInfoDto GetChatUser(string id,
            IChatDtoFactory<ChatUser, UserInfoDto> dtoFactory,
            IChatEntityRepositoryProxy<ChatUser, ChatDbContext> repo);

        /// <summary>
        /// Gets a list of server DTOs stripped of internal data that current user is part of.
        /// </summary>
        /// <param name="dtoFactory">Factory handling creation of <c>ServerIconDto</c></param>
        IEnumerable<ServerInfoDto> GetUserServers(IChatDtoFactory<Server, ServerInfoDto> dtoFactory);
        
        /// <summary>
        /// Gets internal data of the server with provided ID.
        /// </summary>
        /// <param name="serverId">ID of the requested server.</param>
        /// <param name="dtoFactory">Factory handling creation of <c>ServerDataDto</c></param>
        ServerDataDto GetServerData(int serverId,
            IChatDtoFactory<Server, ServerDataDto> dtoFactory,
            IChatEntityRepositoryProxy<Server, ChatDbContext> repo);

        /// <summary>
        /// Gets a list of message DTOs from a channel with provided ID.
        /// </summary>
        /// <param name="channelId">ID of the channel containing messages.</param>
        /// <param name="dtoFactory">Factory handling creation of <c>MessageDto</c></param>
        /// <param name="channelRepo"></param>
        /// <param name="serverId">ID of the server containing the channel.</param>
        IEnumerable<MessageDto> GetChannelMessages(int channelId,
            IChatDtoFactory<Message, MessageDto> dtoFactory,
            IChatEntityRepositoryProxy<Channel, ChatDbContext> channelRepo);
    }
}
