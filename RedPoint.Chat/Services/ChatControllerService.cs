using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class ChatControllerService : IChatControllerService
    {
        private readonly IChatEntityRepositoryProxy _repoProxy;
        private readonly IChatRequestValidator _requestValidator;

        private readonly UserManager<ChatUser> _userManager;

        /// <summary>
        /// Current user is provided by <c>UserManager</c> on <c>ChatControllerService</c> creation.
        /// </summary>
        private ChatUser _user;

        //TODO Remove usage of UserManager from Chat Area, move all account activity including verification of
        //users to Account Area
        public ChatControllerService(UserManager<ChatUser> userManager,
            IChatEntityRepositoryProxy repoProxy,
            IChatRequestValidator requestValidator,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _repoProxy = repoProxy;
            _requestValidator = requestValidator;
            

            AssignChatUser(httpContextAccessor.HttpContext.User).ConfigureAwait(false);
        }

        private async Task AssignChatUser(ClaimsPrincipal user)
        {
            _user = await _userManager.GetUserAsync(user);
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
