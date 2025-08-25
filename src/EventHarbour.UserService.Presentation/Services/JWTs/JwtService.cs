using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using EventHarbout.UserService.Models;
using Microsoft.IdentityModel.Tokens;

namespace EventHarbour.UserService.Presentation.Services.JWTs;

public class JwtService : IJwtService
{
    public string GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var rsa = LoadRsaKey();

        var signingCredentials = new SigningCredentials(
            new RsaSecurityKey(rsa),
            SecurityAlgorithms.RsaSha256
            );

        var tokenDescriptor = new SecurityTokenDescriptor
        {

        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private List<Claim> CreateClaims(User user)
    {
        throw new NotImplementedException();
    }

    private RSA LoadRsaKey()
    {
        var rsa = RSA.Create();
        
        var pemContents = File.ReadAllText("");
        rsa.ImportFromPem(pemContents);
        
        return rsa;
    }
}