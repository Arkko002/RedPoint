using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Utilities.DtoFactories
{
    public class ChannelDtoFactory : IChatDtoFactory<Channel>
    {
        public IDto GetDto(Channel sourceObject)
        {
            var channelDto = new ChannelDto()
            {
                Id = sourceObject.Id,
                Name = sourceObject.Name,
                Description = sourceObject.Description
            };

            return channelDto;
        }
    }
}