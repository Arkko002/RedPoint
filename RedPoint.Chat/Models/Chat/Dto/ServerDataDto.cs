using System.Collections.Generic;

namespace RedPoint.Chat.Models.Chat.Dto
{
    /// <summary>
    /// Server DTO with channel and user list included.
    /// </summary>
    public class ServerDataDto : IDto
    {
        public int Id { get; set; }

        public IEnumerable<ChannelInfoDto> ChannelList { get; set; }
        public IEnumerable<UserInfoDto> UserList { get; set; }
    }
}