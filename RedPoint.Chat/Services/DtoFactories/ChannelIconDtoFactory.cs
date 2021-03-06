using System.Collections.Generic;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates channel DTOs that don't contain internal channel data.
    /// </summary>
    public class ChannelIconDtoFactory : IChatDtoFactory<Channel, ChannelIconDto>
    {
        /// <inheritdoc/>
        public ChannelIconDto CreateDto(Channel sourceObject)
        {
            var channelDto = new ChannelIconDto
            {
                Id = sourceObject.Id,
                Name = sourceObject.Name,
                Description = sourceObject.Description,
                HubGroupId = sourceObject.GroupId
            };

            return channelDto;
        }

        /// <inheritdoc/>
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