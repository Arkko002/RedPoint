using System.Collections.Generic;
using System.Globalization;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Chat.Services.DtoFactories
{
    public class MessageDtoFactory : IChatDtoFactory<Message, MessageDto>
    {
        public MessageDto CreateDto(Message sourceObject)
        {
            var messageDto = new MessageDto
            {
                Id = sourceObject.Id,
                DateTimePosted = sourceObject.DateTimePosted.ToString(CultureInfo.InvariantCulture),
                Text = sourceObject.Text,
                UserId = sourceObject.User.Id
            };

            return messageDto;
        }

        public List<MessageDto> CreateDtoList(List<Message> sourceList)
        {
            var dtoList = new List<MessageDto>();
            foreach (var message in sourceList)
            {
                dtoList.Add(CreateDto(message));
            }

            return dtoList;
        }
    }
}