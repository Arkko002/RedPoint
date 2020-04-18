using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services.Security;
using RedPoint.Data.UnitOfWork;

namespace RedPoint.Areas.Chat.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly IChatEntityRepositoryProxy _repoProxy;
        private readonly EntityUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IChatRequestValidator _requestValidator;
        private readonly IChatErrorHandler _errorHandler;
        
        private ApplicationUser _user;

        public ChatHubService(EntityUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IChatEntityRepositoryProxy repoProxy,
            IChatRequestValidator requestValidator,
            IChatErrorHandler errorHandler)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _repoProxy = repoProxy;
            _requestValidator = requestValidator;
            _errorHandler = errorHandler;
        }

        public void AssignApplicationUser(ClaimsPrincipal user)
        {
            _user = _userManager.GetUserAsync(user).Result;
        }

        public void AddChannel(int serverId, ChannelDto channel)
        {
            var result = CheckServerRequest(serverId, PermissionType.CanManageChannels);
            CheckChatError(result);
            
            var newChannel = new Channel(channel);

            _repoProxy.ChannelRepository.Add(newChannel);
            _unitOfWork.Submit();
        }

        public void AddMessage(int channelId, int serverId, MessageDto message)
        {
            var result = CheckChannelRequest(channelId, serverId, PermissionType.CanWrite);
            CheckChatError(result);
            
            var newMessage = new Message(message, _user);

            _repoProxy.MessageRepository.Add(newMessage);
            _unitOfWork.Submit();
        }

        public void AddServer(ServerDto server)
        {
            var newServer = new Server(server);

            _repoProxy.ServerRepository.Add(newServer);
            _unitOfWork.Submit();
        }

        public void DeleteChannel(int channelId, int serverId)
        {
            var result = CheckChannelRequest(channelId, serverId, PermissionType.CanManageChannels);
            CheckChatError(result);

            _repoProxy.ChannelRepository.Delete(_repoProxy.ChannelRepository.Find(channelId));
            _unitOfWork.Submit();
        }

        public void DeleteServer(int serverId)
        {
            var result = CheckServerRequest(serverId, PermissionType.CanManageServer);
            CheckChatError(result);
            
            _repoProxy.ServerRepository.Delete(_repoProxy.ServerRepository.Find(serverId));
            _unitOfWork.Submit();
        }

        public void DeleteMessage(int messageId, int channelId, int serverId)
        {
            var result = CheckChannelRequest(channelId, serverId, PermissionType.CanManageChannels);
            CheckChatError(result);
            
            _repoProxy.MessageRepository.Delete(_repoProxy.MessageRepository.Find(messageId));
            _unitOfWork.Submit();
        }

        private ChatError CheckServerRequest(int serverId, PermissionType permissionType)
        {
            var server = _repoProxy.TryFindingServer(serverId, _user);
            return _requestValidator.IsServerRequestValid(server, _user, permissionType);
        }

        private ChatError CheckChannelRequest(int channelId, int serverId, PermissionType permissionType)
        {
            var channel = _repoProxy.TryFindingChannel(channelId, _user);
            var server = _repoProxy.TryFindingServer(serverId, _user);
            return _requestValidator.IsChannelRequestValid(channel, server, _user, permissionType);
        }

        private void CheckChatError(ChatError error)
        {
            if (error.ErrorType != ChatErrorType.NoError)
            {
                _errorHandler.HandleChatError(error);
            }
        }
    }
}