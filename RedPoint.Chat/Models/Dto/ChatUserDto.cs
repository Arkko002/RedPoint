using System.Drawing;

namespace RedPoint.Chat.Models.Dto
{
    /// <summary>
    ///     DTO class for ChatUser
    /// </summary>
    public class ChatUserDto : IDto
    {
        //Use those to retrieve ChatUser
        public string AppUserId { get; set; }
        public string Username { get; set; }

        public string CurrentChannelId { get; set; }
        public string CurrentServerId { get; set; }
        public Bitmap Image { get; set; }
    }
}