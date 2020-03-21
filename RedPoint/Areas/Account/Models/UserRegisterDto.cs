using System.ComponentModel.DataAnnotations;

public class UserRegisterDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
    public string Password { get; set; }
}