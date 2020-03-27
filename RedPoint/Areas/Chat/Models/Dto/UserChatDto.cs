namespace RedPoint.Areas.Chat.Models.Dto
{
    /// <summary>
    /// DTO class for ApplicationUser
    /// </summary>
    public class UserChatDto : IDto
    {
        public int Id { get; set; }

        //Use those to retrive  ApplicationUser
        public string AppUserId { get; set; }
        public string Username { get; set; }

        public string CurrentChannelId { get; set; }
        public int CurrentServerId { get; set; }
        public string ImagePath { get; set; }
    }
}