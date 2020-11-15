using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Dto;
using RedPoint.Chat.Services.Security;
using RedPoint.Data.UnitOfWork;

namespace RedPoint.Chat.Services
{
    /// <inheritdoc />
    public class ChatHubService : IChatHubService
    {
        private readonly IChatErrorHandler _errorHandler;
        private readonly IChatEntityRepositoryProxy _repoProxy;
        private readonly IChatRequestValidator _requestValidator;

        private readonly EntityUnitOfWork _unitOfWork;
        private readonly UserManager<ChatUser> _userManager;

        private ChatUser _user;

        public ChatHubService(EntityUnitOfWork unitOfWork,
            UserManager<ChatUser> userManager,
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

            AssignChatUser(httpContextAccessor.HttpContext.User).ConfigureAwait(false);
        }
        
        private async Task AssignChatUser(ClaimsPrincipal user)
        {
            _user = await _userManager.GetUserAsync(user);
        }

        /// <inheritdoc/> 
        public void AddChannel(int serverId, ChannelIconDto channelIcon)
        {
            var result = CheckServerRequest(serverId, PermissionType.CanManageChannels);
            CheckChatError(result);

            var newChannel = new Channel(channelIcon);

            _repoProxy.ChannelRepository.Add(newChannel);
            _unitOfWork.Submit();
        }

        /// <inheritdoc/>
        public void AddMessage(int channelId, int serverId, MessageDto message)
        {
            var result = CheckChannelRequest(channelId, serverId, PermissionType.CanWrite);
            CheckChatError(result);

            var newMessage = new Message(message, _user);

            _repoProxy.MessageRepository.Add(newMessage);
            _unitOfWork.Submit();
        }

        /// <inheritdoc/>
        public void AddServer(ServerIconDto serverIcon)
        {
            var newServer = new Server(serverIcon);

            _repoProxy.ServerRepository.Add(newServer);
            _unitOfWork.Submit();
        }

        /// <inheritdoc/>
        public void DeleteChannel(int channelId, int serverId)
        {
            var result = CheckChannelRequest(channelId, serverId, PermissionType.CanManageChannels);
            CheckChatError(result);

            _repoProxy.ChannelRepository.Delete(_repoProxy.ChannelRepository.Find(channelId));
            _unitOfWork.Submit();
        }

        /// <inheritdoc/> 
        public void DeleteServer(int serverId)
        {
            var result = CheckServerRequest(serverId, PermissionType.CanManageServer);
            CheckChatError(result);

            _repoProxy.ServerRepository.Delete(_repoProxy.ServerRepository.Find(serverId));
            _unitOfWork.Submit();
        }
        
        /// <inheritdoc />>
        public void DeleteMessage(int messageId, int channelId, int serverId)
        {
            var result = CheckChannelRequest(channelId, serverId, PermissionType.CanManageChannels);
            CheckChatError(result);

            _repoProxy.MessageRepository.Delete(_repoProxy.MessageRepository.Find(messageId));
            _unitOfWork.Submit();
        }

        /// <summary>
        /// Checks if user has enough permissions to perform requested server action.
        /// </summary>
        /// <param name="serverId">ID of the requested server.</param>
        /// <param name="permissionType">Type of permission to be checked for.</param>
        /// <returns></returns>
        private ChatError CheckServerRequest(int serverId, PermissionType permissionType)
        {
            var server = _repoProxy.TryFindingServer(serverId, _user);
            return _requestValidator.IsServerRequestValid(server, _user, permissionType);
        }

        /// <summary>
        /// Check if user has enough permissions to perform requested channel action.
        /// </summary>
        /// <param name="channelId">ID of the requested channel.</param>
        /// <param name="serverId">ID of the server containing the channel.</param>
        /// <param name="permissionType">Type of permission to be checked for.</param>
        /// <returns></returns>
        private ChatError CheckChannelRequest(int channelId, int serverId, PermissionType permissionType)
        {
            var channel = _repoProxy.TryFindingChannel(channelId, _user);
            var server = _repoProxy.TryFindingServer(serverId, _user);
            return _requestValidator.IsChannelRequestValid(channel, server, _user, permissionType);
        }

        private void CheckChatError(ChatError error)
        {
            //TODO maybe get rid of this? Move != NoError checks to error handler
            if (error.ErrorType != ChatErrorType.NoError)
            {
                _errorHandler.HandleChatError(error);
            }
        }
    }
}