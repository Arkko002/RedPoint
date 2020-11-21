using System.Collections.Generic;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates channel DTOs with internal channel data.
    /// The amount of data included is based on user's channel permissions/
    /// </summary>
    public class ChannelDataDtoFactory : IChatDtoFactory<Channel, ChannelDataDto>
    {
        private readonly IChatDtoFactory<Message, MessageDto> _messageFactory;

        public ChannelDataDtoFactory(IChatDtoFactory<Message, MessageDto> messageFactory)
        {
            _messageFactory = messageFactory;
        }

        /// <inheritdoc/>
        public ChannelDataDto CreateDto(Channel sourceObject)
        {
            var dto = new ChannelDataDto
            {
                Id = sourceObject.Id,
                Messages = _messageFactory.CreateDtoList(sourceObject.Messages)
            };

            return dto;
        }

        /// <inheritdoc/>
        public List<ChannelDataDto> CreateDtoList(List<Channel> sourceList)
        {
            var dtoList = new List<ChannelDataDto>();

            foreach (var channel in sourceList)
            {
                dtoList.Add(CreateDto(channel));
            }

            return dtoList;
        }
    }
}