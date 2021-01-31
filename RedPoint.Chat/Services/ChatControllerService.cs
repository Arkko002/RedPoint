using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IChatEntityRepositoryProxy _repoProxy;
        private readonly IChatRequestValidator _requestValidator;

        private ChatUser _user;

        public ChatControllerService(IChatEntityRepositoryProxy repoProxy,
            IChatRequestValidator requestValidator)
        {
            _repoProxy = repoProxy;
            _requestValidator = requestValidator;
        }

        public void AssignUserFromToken(JwtSecurityToken userToken)
        {
            //TODO proper proxy
            _user = _repoProxy.TryFindingUser(userToken.Id);
        }

        /// <inheritdoc/>
        public List<ServerIconDto> GetUserServers(IChatDtoFactory<Server, ServerIconDto> dtoFactory)
        {
            return dtoFactory.CreateDtoList(_user.Servers);
        }

        /// <inheritdoc/>
        public ServerDataDto GetServerData(int serverId, IChatDtoFactory<Server, ServerDataDto> dtoFactory)
        {
            var server = _repoProxy.TryFindingServer(serverId, _user);

            //TODO validator, error handling
            return dtoFactory.CreateDto(server);
        }

        /// <inheritdoc/>
        public List<MessageDto> GetChannelMessages(int channelId, int serverId,
            IChatDtoFactory<Message, MessageDto> dtoFactory)
        {
            var channel = _repoProxy.TryFindingChannel(channelId, _user);
            var server = _repoProxy.TryFindingServer(serverId, _user);

            _requestValidator.IsChannelRequestValid(channel, server, _user, PermissionTypes.CanView);

            //TODO Pagination, return 20-40 messages in one batch
            return dtoFactory.CreateDtoList(channel.Messages);
        }
    }
}
