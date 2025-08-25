namespace EventHarbour.UserService.Presentation.DTOs;

public class JwtDto
{
    public required string Token { get; init; }
    public required DateTime ExpirationDate { get; init; }
}