using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.Security;
using RedPoint.Data.UnitOfWork;

namespace RedPoint.Chat.Services
{
    /// <inheritdoc />
    public class ChatHubService : IChatHubService
    {
        private readonly IChatEntityRepositoryProxy _repoProxy;
        private readonly IChatRequestValidator _requestValidator;

        private readonly EntityUnitOfWork _unitOfWork;
        private readonly UserManager<ChatUser> _userManager;

        private ChatUser _user;

        public ChatHubService(EntityUnitOfWork unitOfWork,
            UserManager<ChatUser> userManager,
            IChatEntityRepositoryProxy repoProxy,
            IChatRequestValidator requestValidator,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
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
        public void AddChannel(int serverId, ChannelIconDto channelIcon)
        {
            CheckServerRequest(serverId, PermissionTypes.CanManageChannels);

            var newChannel = new Channel(channelIcon);

            _repoProxy.ChannelRepository.Add(newChannel);
            _unitOfWork.Submit();
        }

        /// <inheritdoc/>
        public void AddMessage(int channelId, int serverId, MessageDto message)
        {
            CheckChannelRequest(channelId, serverId, PermissionTypes.CanWrite);

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
            CheckChannelRequest(channelId, serverId, PermissionTypes.CanManageChannels);

            _repoProxy.ChannelRepository.Delete(_repoProxy.ChannelRepository.Find(channelId));
            _unitOfWork.Submit();
        }

        /// <inheritdoc/> 
        public void DeleteServer(int serverId)
        {
            CheckServerRequest(serverId, PermissionTypes.CanManageServer);

            _repoProxy.ServerRepository.Delete(_repoProxy.ServerRepository.Find(serverId));
            _unitOfWork.Submit();
        }
        
        /// <inheritdoc />>
        public void DeleteMessage(int messageId, int channelId, int serverId)
        {
            CheckChannelRequest(channelId, serverId, PermissionTypes.CanManageChannels);

            _repoProxy.MessageRepository.Delete(_repoProxy.MessageRepository.Find(messageId));
            _unitOfWork.Submit();
        }

        /// <summary>
        /// Checks if user has enough permissions to perform requested server action.
        /// </summary>
        /// <param name="serverId">ID of the requested server.</param>
        /// <param name="permissionTypes">Type of permission to be checked for.</param>
        /// <returns></returns>
        private void CheckServerRequest(int serverId, PermissionTypes permissionTypes)
        {
            var server = _repoProxy.TryFindingServer(serverId, _user);
            _requestValidator.IsServerRequestValid(server, _user, permissionTypes);
        }

        /// <summary>
        /// Check if user has enough permissions to perform requested channel action.
        /// </summary>
        /// <param name="channelId">ID of the requested channel.</param>
        /// <param name="serverId">ID of the server containing the channel.</param>
        /// <param name="permissionTypes">Type of permission to be checked for.</param>
        /// <returns></returns>
        private void CheckChannelRequest(int channelId, int serverId, PermissionTypes permissionTypes)
        {
            var channel = _repoProxy.TryFindingChannel(channelId, _user);
            var server = _repoProxy.TryFindingServer(serverId, _user);
            _requestValidator.IsChannelRequestValid(channel, server, _user, permissionTypes);
        }
    }
}