using System.ComponentModel.DataAnnotations;

namespace RedPoint.Account.Models
{
    /// <summary>
    /// DTO used to transport registration data from front end.
    /// </summary>
    public class UserRegisterDto
    {
        [Required] public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}