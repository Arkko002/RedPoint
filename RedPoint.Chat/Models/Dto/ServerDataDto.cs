using System.Collections.Generic;

namespace RedPoint.Chat.Models.Dto
{
    public class ServerDataDto : IDto
    {
        public int Id { get; set; }

        public List<ChannelIconDto> ChannelList { get; set; }
        public List<ChatUserDto> UserList { get; set; }
    }
}