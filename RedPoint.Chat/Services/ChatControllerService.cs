using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedPoint.Chat.Data;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Models.Errors;
using RedPoint.Chat.Services.DtoFactories;
using RedPoint.Chat.Services.Security;

namespace RedPoint.Chat.Services
{
    /// <inheritdoc cref="IChatControllerService"/>
    [Authorize]
    public class ChatControllerService : IChatControllerService
    {
        private readonly IChatRequestValidator _requestValidator;

        private ChatUser _user;

        public ChatControllerService(IChatRequestValidator requestValidator)
        {
            _requestValidator = requestValidator;
        }

        public void AssignUserFromToken(JwtSecurityToken userToken, ChatEntityRepositoryProxy<ChatUser, ChatDbContext> repoProxy)
        {
            _user = repoProxy.Find(userToken.Id);
        }

        /// <inheritdoc/>
        public List<ServerIconDto> GetUserServers(IChatDtoFactory<Server, ServerIconDto> dtoFactory)
        {
            return dtoFactory.CreateDtoList(_user.Servers);
        }

        /// <inheritdoc/>
        public ServerDataDto GetServerData(int serverId,
            IChatDtoFactory<Server, ServerDataDto> dtoFactory,
            ChatEntityRepositoryProxy<Server, ChatDbContext> repoProxy)
        {
            var server = repoProxy.Find(serverId);

            //TODO validator, error handling
            return dtoFactory.CreateDto(server);
        }

        /// <inheritdoc/>
        public List<MessageDto> GetChannelMessages(int channelId,
            IChatDtoFactory<Message, MessageDto> dtoFactory,
            ChatEntityRepositoryProxy<Channel, ChatDbContext> channelRepo)
        {
            var channel = channelRepo.Find(channelId);

            _requestValidator.IsChannelRequestValid(channel, channel.Server, _user, PermissionTypes.CanView);

            //TODO Pagination, return 20-40 messages in one batch
            return dtoFactory.CreateDtoList(channel.Messages);
        }
    }
}
