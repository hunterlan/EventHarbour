using System.Security.Cryptography;

namespace EventHarbour.UserService.Presentation.Services.JWTs;

public class JwtService : IJwtService
{
    public string GenerateToken()
    {
        throw new NotImplementedException();
    }

    private RSA LoadRsaKey()
    {
        throw new NotImplementedException();
    }
}