using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services.DtoFactories;
using RedPoint.Areas.Chat.Services.Security;

namespace RedPoint.Areas.Chat.Services
{
    public class ChatControllerService : IChatControllerService
    {
        private readonly IChatEntityRepositoryProxy _repoProxy;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IChatRequestValidator _requestValidator;
        private readonly IChatErrorHandler _errorHandler;
        
        private ApplicationUser _user;
        
        public ChatControllerService(UserManager<ApplicationUser> userManager,
            IChatEntityRepositoryProxy repoProxy,
            IChatRequestValidator requestValidator,
            IChatErrorHandler errorHandler)
        {
            _userManager = userManager;
            _repoProxy = repoProxy;
            _requestValidator = requestValidator;
            _errorHandler = errorHandler;
        }

        public void AssignApplicationUser(ClaimsPrincipal user)
        {
            _user = _userManager.GetUserAsync(user).Result;
        }
        
        public List<ServerIconDto> GetUserServers(IChatDtoFactory<Server, ServerIconDto> dtoFactory)
        {
            return dtoFactory.CreateDtoList(_user.Servers);
        }

        public ServerDataDto GetServerData(int serverId, IChatDtoFactory<Server, ServerDataDto> dtoFactory)
        {
            var server = _repoProxy.TryFindingServer(serverId, _user);

            return dtoFactory.CreateDto(server);
        }
        
        public List<MessageDto> GetChannelMessages(int channelId, int serverId, IChatDtoFactory<Message, MessageDto> dtoFactory)
        {
            var channel = _repoProxy.TryFindingChannel(channelId, _user);
            var server = _repoProxy.TryFindingServer(serverId, _user);
            
            var result = _requestValidator.IsChannelRequestValid(channel, server, _user, PermissionType.CanView);

            if (result.ErrorType != ChatErrorType.NoError)
            {
                _errorHandler.HandleChatError(result);
            }

            //TODO Pagination, return 20-40 messages in one batch
            return dtoFactory.CreateDtoList(channel.Messages);
        }
    }
}