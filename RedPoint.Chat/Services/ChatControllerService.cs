using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using RedPoint.Chat.Data;
using RedPoint.Chat.Exceptions;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services.DtoFactories;
using RedPoint.Chat.Services.Security;

namespace RedPoint.Chat.Services
{
    //TODO Dependency injection directly to services
    
    /// <inheritdoc cref="IChatControllerService"/>
    public class ChatControllerService : IChatControllerService
    {
        private readonly IChatRequestValidator _requestValidator;

        private ChatUser _user;

        public ChatControllerService(IChatRequestValidator requestValidator,
            IHttpContextAccessor httpContextAccessor,
            IChatEntityRepositoryProxy<ChatUser, ChatDbContext> userRepo)
        {
            _requestValidator = requestValidator;

            var tokenStr = httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
            var token = new JwtSecurityToken(tokenStr);
            _user = GetUserFromToken(token, userRepo);
            
        }

        private ChatUser GetUserFromToken(JwtSecurityToken token, IChatEntityRepositoryProxy<ChatUser, ChatDbContext> userRepo)
        {
            ChatUser user;
            try
            {
                user = userRepo.Find(token.Id);
            }
            catch (EntityNotFoundException e)
            {
                user = new ChatUser
                {
                    Id = token.Subject,
                    UserName = token.Claims.SingleOrDefault(x => x.Type == "Name").Value
                };
                
                userRepo.Add(user);
            }

            return user;
        }

        public ChatUserDto GetChatUser(string id,
            IChatDtoFactory<ChatUser, ChatUserDto> dtoFactory,
            IChatEntityRepositoryProxy<ChatUser, ChatDbContext> userRepo)
        {
            var user = userRepo.Find(id);
            return dtoFactory.CreateDto(user);
        }
        
        /// <inheritdoc/>
        public List<ServerIconDto> GetUserServers(IChatDtoFactory<Server, ServerIconDto> dtoFactory)
        {
            return dtoFactory.CreateDtoList(_user.Servers);
        }

        /// <inheritdoc/>
        public ServerDataDto GetServerData(int serverId,
            IChatDtoFactory<Server, ServerDataDto> dtoFactory,
            IChatEntityRepositoryProxy<Server, ChatDbContext> repoProxy)
        {
            var server = repoProxy.Find(serverId);

            //TODO validator, error handling
            return dtoFactory.CreateDto(server);
        }

        /// <inheritdoc/>
        public List<MessageDto> GetChannelMessages(int channelId,
            IChatDtoFactory<Message, MessageDto> dtoFactory,
            IChatEntityRepositoryProxy<Channel, ChatDbContext> channelRepo)
        {
            var channel = channelRepo.Find(channelId);

            _requestValidator.IsChannelRequestValid(channel, channel.Server, _user, PermissionTypes.CanView);

            //TODO Pagination, return 20-40 messages in one batch
            return dtoFactory.CreateDtoList(channel.Messages);
        }
    }
}
