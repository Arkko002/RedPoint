using System.Drawing;

namespace RedPoint.Chat.Models.Chat.Dto
{
    /// <summary>
    ///  ChatUser DTO that doesn't include any sensitive account data.
    /// </summary>
    public class ChatUserDto : IDto
    {
        //Use those to retrieve ChatUser
        public string AppUserId { get; set; }
        public string Username { get; set; }

        public string CurrentChannelId { get; set; }
        public string CurrentServerId { get; set; }
        public byte[] Image { get; set; }
    }
}