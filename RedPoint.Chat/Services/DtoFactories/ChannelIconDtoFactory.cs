using System.Collections.Generic;
using RedPoint.Chat.Models;
using RedPoint.Chat.Models.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    public class ChannelIconDtoFactory : IChatDtoFactory<Channel, ChannelIconDto>
    {
        public ChannelIconDto CreateDto(Channel sourceObject)
        {
            var channelDto = new ChannelIconDto
            {
                Id = sourceObject.Id,
                Name = sourceObject.Name,
                Description = sourceObject.Description
            };

            return channelDto;
        }

        public List<ChannelIconDto> CreateDtoList(List<Channel> sourceList)
        {
            var dtoList = new List<ChannelIconDto>();
            foreach (var channel in sourceList)
            {
                dtoList.Add(CreateDto(channel));
            }

            return dtoList;
        }
    }
}