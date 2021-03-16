using System.Collections.Generic;

namespace RedPoint.Chat.Models.Chat.Dto
{
    /// <summary>
    /// Channel DTO with message list included.
    /// </summary>
    public class ChannelDataDto : IDto
    {
        public int Id { get; set; }

        public IEnumerable<MessageDto> Messages { get; set; }
    }
}