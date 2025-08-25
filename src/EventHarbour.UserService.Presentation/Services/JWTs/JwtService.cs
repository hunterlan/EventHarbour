using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using EventHarbour.UserService.Presentation.Options;
using EventHarbout.UserService.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EventHarbour.UserService.Presentation.Services.JWTs;

public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(IOptions<JwtOptions> optionsJwt)
    {
        _jwtOptions = optionsJwt.Value;
    }
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var rsa = LoadRsaKey();

        var signingCredentials = new SigningCredentials(
            new RsaSecurityKey(rsa),
            SecurityAlgorithms.RsaSha256
            );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = CreateClaims(user),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = signingCredentials,
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireAt)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity CreateClaims(User user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Username));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Role, user.Role.Title));
        
        return claims;
    }

    private RSA LoadRsaKey()
    {
        var rsa = RSA.Create();
        
        var pemContents = File.ReadAllText(_jwtOptions.RsaKey.PrivatePath);
        rsa.ImportFromPem(pemContents);
        
        return rsa;
    }
}