using System.ComponentModel.DataAnnotations;

namespace RedPoint.Account.Models
{
    /// <summary>
    /// DTO used to transport login data from front end.
    /// </summary>
    public class UserLoginDto
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}