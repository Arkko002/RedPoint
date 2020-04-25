using System.Collections.Generic;

namespace RedPoint.Areas.Chat.Models.Dto
{
    public class ChannelDataDto : IDto
    {
        public int Id { get; set; }
        
        public List<MessageDto> Messages { get; set; }
    }
}