using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Utilities.DtoFactories
{
    public class ChannelDtoFactory : IChatDtoFactory<Channel>
    {
        public IDto GetDto(Channel sourceObject)
        {
            var channelDto = new ChannelDto()
            {
                Name = sourceObject.Name,
                Description = sourceObject.Description
            };

            return channelDto;
        }
    }
}