using System.ComponentModel.DataAnnotations;

namespace EventHarbour.UserService.Presentation.DTOs;

public class UserDto
{
    [Required]
    [MinLength(4)]
    public string Username { get; set; }
    //TODO: Add regex
    [Required]
    [MinLength(12)]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public bool IsAdult {get; set; }
    [Required]
    public int RoleId { get; set; }
}