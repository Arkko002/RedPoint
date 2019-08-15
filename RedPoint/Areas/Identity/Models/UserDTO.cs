namespace RedPoint.Areas.Identity.Models
{
    /// <summary>
    /// DTO class for ApplicationUser
    /// </summary>
    public class UserDTO
    {
        public int Id { get; set; }

        //Use those to retrive  ApplicationUser
        public string AppUserId { get; set; }
        public string AppUserName { get; set; }
    }
}