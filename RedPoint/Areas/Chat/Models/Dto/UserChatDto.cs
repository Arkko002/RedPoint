using System.Drawing;

namespace RedPoint.Areas.Chat.Models.Dto
{
    /// <summary>
    ///     DTO class for ApplicationUser
    /// </summary>
    public class UserChatDto : IDto
    {
        //Use those to retrive  ApplicationUser
        public string AppUserId { get; set; }
        public string Username { get; set; }

        public string CurrentChannelId { get; set; }
        public int CurrentServerId { get; set; }
        public Bitmap Image { get; set; }
    }
}