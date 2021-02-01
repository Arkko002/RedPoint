using System.Collections.Generic;
using System.Globalization;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;

namespace RedPoint.Chat.Services.DtoFactories
{
    /// <summary>
    /// Factory that creates message DTOs.
    /// </summary>
    public class MessageDtoFactory : IChatDtoFactory<Message, MessageDto>
    {
        /// <inheritdoc/>
        public MessageDto CreateDto(Message sourceObject)
        {
            var messageDto = new MessageDto
            {
                Id = sourceObject.Id,
                DateTimePosted = sourceObject.DateTimePosted.ToString(CultureInfo.InvariantCulture),
                Text = sourceObject.Text,
                UserId = sourceObject.User.Id,
                ChannelId = sourceObject.Channel.Id
            };

            return messageDto;
        }

        /// <inheritdoc/>
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