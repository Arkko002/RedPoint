using System.Collections.Generic;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates channel DTOs that don't contain internal channel data.
    /// </summary>
    public class ChannelInfoDtoFactory : IChatDtoFactory<Channel, ChannelInfoDto>
    {
        /// <inheritdoc/>
        public ChannelInfoDto CreateDto(Channel sourceObject)
        {
            var channelDto = new ChannelInfoDto
            {
                Id = sourceObject.Id,
                Name = sourceObject.Name,
                Description = sourceObject.Description,
                HubGroupId = sourceObject.GroupId
            };

            return channelDto;
        }

        /// <inheritdoc/>
        public IEnumerable<ChannelInfoDto> CreateDtoList(IEnumerable<Channel> sourceList)
        {
            var dtoList = new List<ChannelInfoDto>();
            foreach (var channel in sourceList)
            {
                dtoList.Add(CreateDto(channel));
            }

            return dtoList;
        }
    }
}