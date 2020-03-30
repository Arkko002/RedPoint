using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Utilities.DtoFactories
{
    public class MessageDtoFactory : IChatDtoFactory<Message>
    {
        public IDto GetDto(Message sourceObject)
        {
            var messageDto = new MessageDto()
            {
                Id = sourceObject.Id,
                DateTimePosted = sourceObject.DateTimePosted.ToString(),
                Text = sourceObject.Text,
                UserId = sourceObject.User.Id
            };

            return messageDto;
        }
    }
}