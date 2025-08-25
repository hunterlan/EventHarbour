using System.ComponentModel.DataAnnotations;

namespace EventHarbour.UserService.Presentation.DTOs;

public class AuthDto
{
    [Required]
    public string Login { get; init; }
    [Required]
    public string Password { get; init; }
}