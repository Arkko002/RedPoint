using System.Collections.Generic;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Chat.Services.DtoFactories
{
    public class UserDtoFactory : IChatDtoFactory<ApplicationUser, UserChatDto>
    {
        public UserChatDto CreateDto(ApplicationUser sourceObject)
        {
            var userDto = new UserChatDto
            {
                AppUserId = sourceObject.Id,
                Username = sourceObject.UserName,
                Image = sourceObject.Image,
                CurrentChannelId = sourceObject.CurrentChannelId,
                CurrentServerId = sourceObject.CurrentServerId
            };

            return userDto;
        }

        public List<UserChatDto> CreateDtoList(List<ApplicationUser> sourceList)
        {
            var dtoList = new List<UserChatDto>();
            foreach (var user in sourceList)
            {
                dtoList.Add(CreateDto(user));
            }

            return dtoList;
        }
    }
}