namespace RedPoint.Areas.Identity.Models
{
    /// <summary>
    /// DTO class for ApplicationUser
    /// </summary>
    public class UserChatDto
    {
        public int Id { get; set; }

        //Use those to retrive  ApplicationUser
        public string AppUserId { get; set; }
        public string Username { get; set; }
    }
}