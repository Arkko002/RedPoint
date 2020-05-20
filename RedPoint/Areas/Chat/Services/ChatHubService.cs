using System.Security.Claims;
using Microsoft.AspNetCore.Http;
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
        private readonly IChatErrorHandler _errorHandler;
        private readonly IChatEntityRepositoryProxy _repoProxy;
        private readonly IChatRequestValidator _requestValidator;
        
        private readonly EntityUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        private IHttpContextAccessor _httpContextAccessor;
            
        private ApplicationUser _user;

        public ChatHubService(EntityUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IChatEntityRepositoryProxy repoProxy,
            IChatRequestValidator requestValidator,
            IChatErrorHandler errorHandler,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _repoProxy = repoProxy;
            _requestValidator = requestValidator;
            _errorHandler = errorHandler;
            _httpContextAccessor = httpContextAccessor;
            
            AssignApplicationUser(_httpContextAccessor.HttpContext.User);
        }

        private async void AssignApplicationUser(ClaimsPrincipal user)
        {
            _user = await _userManager.GetUserAsync(user);
        }

        public void AddChannel(int serverId, ChannelIconDto channelIcon)
        {
            var result = CheckServerRequest(serverId, PermissionType.CanManageChannels);
            CheckChatError(result);

            var newChannel = new Channel(channelIcon);

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

        public void AddServer(ServerIconDto serverIcon)
        {
            var newServer = new Server(serverIcon);

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