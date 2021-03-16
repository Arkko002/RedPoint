
using System.Collections.Generic;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates user account DTOs that are stripped of sensitive information.
    /// </summary>
    public class CurrentUserDtoFactory : IChatDtoFactory<ChatUser, CurrentUserDto>
    {
        private IChatDtoFactory<Server, ServerInfoDto> _serverDtoFactory;

        public CurrentUserDtoFactory(IChatDtoFactory<Server, ServerInfoDto> serverDtoFactory)
        {
            _serverDtoFactory = serverDtoFactory;
        }

        /// <inheritdoc/>
        public CurrentUserDto CreateDto(ChatUser sourceObject)
        {
            var userDto = new CurrentUserDto 
            {
                AppUserId = sourceObject.Id,
                Username = sourceObject.UserName,
                Image = sourceObject.Image,
                CurrentChannelId = sourceObject.CurrentChannelId,
                CurrentServerId = sourceObject.CurrentServerId,
                Servers = _serverDtoFactory.CreateDtoList(sourceObject.Servers)
            };

            return userDto;
        }

        /// <inheritdoc/>
        public IEnumerable<CurrentUserDto> CreateDtoList(IEnumerable<ChatUser> sourceList)
        {
            var dtoList = new List<CurrentUserDto>();
            foreach (var user in sourceList)
            {
                dtoList.Add(CreateDto(user));
            }

            return dtoList;
        }
    }
}
