using System.Drawing;

namespace RedPoint.Chat.Models.Chat.Dto
{
    /// <summary>
    ///  Chat DTO that doesn't include any sensitive account data.
    /// </summary>
    public class UserInfoDto : IDto
    {
        //Use those to retrieve ChatUser
        public string AppUserId { get; set; }
        public string Username { get; set; }

        public byte[] Image { get; set; }
    }
}
