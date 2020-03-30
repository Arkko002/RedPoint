using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Identity.Models;

namespace RedPoint.Utilities.DtoFactories
{
    public class UserDtoFactory : IChatDtoFactory<ApplicationUser>
    {
        public IDto GetDto(ApplicationUser sourceObject)
        {
            var userDto = new UserChatDto()
            {
                AppUserId = sourceObject.Id,
                Username = sourceObject.UserName,
                ImagePath = sourceObject.ImagePath,
                CurrentChannelId = sourceObject.CurrentChannelId,
                CurrentServerId = sourceObject.CurrentServerId
            };

            return userDto;
        }
    }
}