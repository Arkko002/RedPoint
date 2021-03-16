using System.Collections.Generic;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates user account DTOs that are stripped of sensitive information.
    /// </summary>
    public class UserInfoDtoFactory : IChatDtoFactory<ChatUser, UserInfoDto>
    {
        /// <inheritdoc/>
        public UserInfoDto CreateDto(ChatUser sourceObject)
        {
            var userDto = new UserInfoDto
            {
                AppUserId = sourceObject.Id,
                Username = sourceObject.UserName,
                Image = sourceObject.Image,
            };

            return userDto;
        }

        /// <inheritdoc/>
        public IEnumerable<UserInfoDto> CreateDtoList(IEnumerable<ChatUser> sourceList)
        {
            var dtoList = new List<UserInfoDto>();
            foreach (var user in sourceList)
            {
                dtoList.Add(CreateDto(user));
            }

            return dtoList;
        }
    }
}
