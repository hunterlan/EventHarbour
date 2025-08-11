namespace EventHarbout.UserService.Models;

public class Profile
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsAdult { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}