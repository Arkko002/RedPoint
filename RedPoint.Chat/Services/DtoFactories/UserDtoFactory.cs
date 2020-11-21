using System.Collections.Generic;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates user account DTOs that are stripped of sensitive information.
    /// </summary>
    public class UserDtoFactory : IChatDtoFactory<ChatUser, ChatUserDto>
    {
        /// <inheritdoc/>
        public ChatUserDto CreateDto(ChatUser sourceObject)
        {
            var userDto = new ChatUserDto
            {
                AppUserId = sourceObject.Id,
                Username = sourceObject.UserName,
                Image = sourceObject.Image,
                CurrentChannelId = sourceObject.CurrentChannelId,
                CurrentServerId = sourceObject.CurrentServerId
            };

            return userDto;
        }

        /// <inheritdoc/>
        public List<ChatUserDto> CreateDtoList(List<ChatUser> sourceList)
        {
            var dtoList = new List<ChatUserDto>();
            foreach (var user in sourceList)
            {
                dtoList.Add(CreateDto(user));
            }

            return dtoList;
        }
    }
}