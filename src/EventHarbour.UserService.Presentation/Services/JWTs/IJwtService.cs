using EventHarbout.UserService.Models;

namespace EventHarbour.UserService.Presentation.Services.JWTs;

public interface IJwtService
{
    string GenerateToken(User user);
}