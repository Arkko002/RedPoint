using System.Collections.Generic;

namespace RedPoint.Areas.Chat.Models.Dto
{
    public class ServerDataDto : IDto
    {
        public int Id { get; set; }

        public List<ChannelIconDto> ChannelList { get; set; }
        public List<UserChatDto> UserList { get; set; }
    }
}