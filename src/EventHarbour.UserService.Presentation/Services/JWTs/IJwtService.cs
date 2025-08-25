using EventHarbour.UserService.Presentation.DTOs;
using EventHarbout.UserService.Models;

namespace EventHarbour.UserService.Presentation.Services.JWTs;

public interface IJwtService
{
    JwtDto GenerateToken(UserDto user);
    string GetPublicKey();
}