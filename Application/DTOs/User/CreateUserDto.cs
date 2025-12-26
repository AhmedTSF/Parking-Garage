
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User;

public class CreateUserDto
{
    [Required, StringLength(14)]
    public string NationalId { get; set; }

    [Required, StringLength(50)]
    public string FirstName { get; set; }

    [Required, StringLength(50)]
    public string LastName { get; set; }

    [Required, StringLength(50)]
    public string Username { get; set; }

    [Required, Phone]
    public string PhoneNumber { get; set; }

    [Required]
    public string Role { get; set; }


    // I didn't apply OTP in this project. I already apply it in Bank System Project
    [Required, StringLength(100, MinimumLength = 8)]
    public string Password { get; set; }
}
