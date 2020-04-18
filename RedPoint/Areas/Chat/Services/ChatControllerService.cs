using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services.Security;
using RedPoint.Data;
using RedPoint.Data.Repository;
using RedPoint.Exceptions;
using RedPoint.Exceptions.Security;
using RedPoint.Services.DtoManager;
using RedPoint.Utilities.DtoFactories;

namespace RedPoint.Areas.Chat.Services
{
    public class ChatControllerService : IChatControllerService
    {
        private readonly IChatEntityRepositoryProxy _repoProxy;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDtoManager _dtoManager;
        
        private readonly IChatRequestValidator _requestValidator;
        private readonly IChatErrorHandler _errorHandler;
        
        private ApplicationUser _user;
        
        public ChatControllerService(UserManager<ApplicationUser> userManager,
            IChatEntityRepositoryProxy repoProxy,
            IDtoManager dtoManager,
            IChatRequestValidator requestValidator,
            IChatErrorHandler errorHandler)
        {
            _userManager = userManager;
            _repoProxy = repoProxy;
            _dtoManager = dtoManager;
            _requestValidator = requestValidator;
            _errorHandler = errorHandler;
        }

        public void AssignApplicationUser(ClaimsPrincipal user)
        {
            _user = _userManager.GetUserAsync(user).Result;
        }
        
        public List<ServerDto> GetUserServers(IChatDtoFactory<Server> dtoFactory)
        {
            return _dtoManager.CreateDtoList<Server, ServerDto>(_user.Servers, dtoFactory);
        }
        
        public List<ChannelDto> GetServerChannels(int serverId, IChatDtoFactory<Channel> dtoFactory)
        {
            var server = _repoProxy.TryFindingServer(serverId, _user);
            
            List<Channel> userPermittedChannels = new List<Channel>();
            foreach (var channel in server.Channels)
            {
                //TODO flow control
                if (_requestValidator.IsChannelRequestValid(channel, server, _user, PermissionType.CanView).ErrorType == ChatErrorType.NoError)
                {
                    userPermittedChannels.Add(channel);
                }
            }
            
            return _dtoManager.CreateDtoList<Channel, ChannelDto>(userPermittedChannels, dtoFactory);
        }
        
        public List<UserChatDto> GetServerUserList(int serverId, IChatDtoFactory<ApplicationUser> dtoFactory)
        {
            var server = _repoProxy.TryFindingServer(serverId, _user);
            var result = _requestValidator.IsServerRequestValid(server, _user, PermissionType.CanView);

            if (result.ErrorType != ChatErrorType.NoError)
            {
                _errorHandler.HandleChatError(result);
            }

            return _dtoManager.CreateDtoList<ApplicationUser, UserChatDto>(server.Users, dtoFactory);
        }

        
        public List<MessageDto> GetChannelMessages(int channelId, int serverId, IChatDtoFactory<Message> dtoFactory)
        {
            var channel = _repoProxy.TryFindingChannel(channelId, _user);
            var server = _repoProxy.TryFindingServer(serverId, _user);
            
            var result = _requestValidator.IsChannelRequestValid(channel, server, _user, PermissionType.CanView);

            if (result.ErrorType != ChatErrorType.NoError)
            {
                _errorHandler.HandleChatError(result);
            }
            
            //TODO Lazy loading, return only 20-40 currently visible messages
            return _dtoManager.CreateDtoList<Message, MessageDto>(channel.Messages, dtoFactory);
        }
    }
}