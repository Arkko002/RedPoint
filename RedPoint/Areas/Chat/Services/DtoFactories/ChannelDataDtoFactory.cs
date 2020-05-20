using System.Collections.Generic;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Chat.Services.DtoFactories
{
    public class ChannelDataDtoFactory : IChatDtoFactory<Channel, ChannelDataDto>
    {
        private readonly IChatDtoFactory<Message, MessageDto> _messageFactory;

        public ChannelDataDtoFactory(IChatDtoFactory<Message, MessageDto> messageFactory)
        {
            _messageFactory = messageFactory;
        }

        public ChannelDataDto CreateDto(Channel sourceObject)
        {
            var dto = new ChannelDataDto
            {
                Id = sourceObject.Id, Messages = _messageFactory.CreateDtoList(sourceObject.Messages)
            };

            return dto;
        }

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