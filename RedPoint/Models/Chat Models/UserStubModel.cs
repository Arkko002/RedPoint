namespace RedPoint.Models.Chat_Models
{
    /// <summary>
    /// DTO class for ApplicationUser
    /// </summary>
    public class UserStub
    {
        public int Id { get; set; }

        //Use those to retrive  ApplicationUser
        public string AppUserId { get; set; }
        public string AppUserName { get; set; }
    }
}