using System.Collections.Generic;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    public class UserDtoFactory : IChatDtoFactory<ChatUser, ChatUserDto>
    {
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