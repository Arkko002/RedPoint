using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedPoint.Chat.Data;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.Security;
using RedPoint.Data.UnitOfWork;

namespace RedPoint.Chat.Services
{
    /// <inheritdoc />
    public class ChatHubService : IChatHubService
    {
        private readonly IChatRequestValidator _requestValidator;

        private readonly EntityUnitOfWork _unitOfWork;

        private ChatUser _user;

        public ChatHubService(EntityUnitOfWork unitOfWork, IChatRequestValidator requestValidator)
        {
            _unitOfWork = unitOfWork;
            _requestValidator = requestValidator;
        }
        
        public void AssignChatUserFromToken(JwtSecurityToken token, ChatEntityRepositoryProxy<ChatUser, ChatDbContext> repo)
        {
            _user = repo.Find(token.Id);
        }

        /// <inheritdoc/> 
        public void AddChannel(ChannelIconDto channelIcon,
            ChatEntityRepositoryProxy<Channel, ChatDbContext> channelRepo,
            ChatEntityRepositoryProxy<Server, ChatDbContext> serverRepo)
        {
            var server = serverRepo.Find(channelIcon.ServerId);
            _requestValidator.IsServerRequestValid(server, _user, PermissionTypes.CanManageChannels);
            
            var newChannel = new Channel(channelIcon);

            channelRepo.Add(newChannel);
            _unitOfWork.Submit();
        }

        /// <inheritdoc/>
        public void AddMessage(MessageDto message,
            ChatEntityRepositoryProxy<Message, ChatDbContext> messageRepo,
            ChatEntityRepositoryProxy<Channel, ChatDbContext> channelRepo)
        {
            var channel = channelRepo.Find(message.ChannelId);
            _requestValidator.IsChannelRequestValid(channel, channel.Server, _user, PermissionTypes.CanWrite);

            var newMessage = new Message(message, _user);

            messageRepo.Add(newMessage);
            _unitOfWork.Submit();
        }

        /// <inheritdoc/>
        public void AddServer(ServerIconDto serverIcon, ChatEntityRepositoryProxy<Server, ChatDbContext> repo)
        {
            var newServer = new Server(serverIcon);

            repo.Add(newServer);
            _unitOfWork.Submit();
        }

        /// <inheritdoc/>
        public void DeleteChannel(int channelId, ChatEntityRepositoryProxy<Channel, ChatDbContext> repo)
        {
            var channel = repo.Find(channelId);
            _requestValidator.IsServerRequestValid(channel.Server, _user, PermissionTypes.CanManageChannels);
            
            repo.Delete(channel);
            _unitOfWork.Submit();
        }
        
        /// <inheritdoc/> 
        public void DeleteServer(int serverId, ChatEntityRepositoryProxy<Server, ChatDbContext> repo)
        {
            var server = repo.Find(serverId);
            _requestValidator.IsServerRequestValid(server, _user, PermissionTypes.CanManageServer);
                
            repo.Delete(repo.Find(serverId));
            _unitOfWork.Submit();
        }
        
        /// <inheritdoc />>
        public void DeleteMessage(int messageId, ChatEntityRepositoryProxy<Message, ChatDbContext> repo)
        {

            var message = repo.Find(messageId);
            _requestValidator.IsChannelRequestValid(message.Channel, message.Channel.Server, _user, PermissionTypes.CanWrite);
            
            repo.Delete(message);
            _unitOfWork.Submit();
        }
    }
}