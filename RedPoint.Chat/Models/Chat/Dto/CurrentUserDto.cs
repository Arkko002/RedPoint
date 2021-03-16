using System.Collections.Generic;
using System.Drawing;

namespace RedPoint.Chat.Models.Chat.Dto
{
    /// <summary>
    /// Contains information about currently logged in user, includes sensitive data. 
    /// </summary>
    public class CurrentUserDto : IDto
    {
        //Use those to retrieve ChatUser
        public string AppUserId { get; set; }
        public string Username { get; set; }

        public string CurrentChannelId { get; set; }
        public string CurrentServerId { get; set; }

        public IEnumerable<ServerInfoDto> Servers {get; set;}

        public byte[] Image { get; set; }
    }
}
