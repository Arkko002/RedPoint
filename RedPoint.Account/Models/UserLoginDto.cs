using System.ComponentModel.DataAnnotations;

namespace RedPoint.Account.Models
{
    public class UserLoginDto
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}