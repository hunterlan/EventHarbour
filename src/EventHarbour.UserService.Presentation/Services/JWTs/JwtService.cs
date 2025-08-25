using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using EventHarbour.UserService.Presentation.DTOs;
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
    public JwtDto GenerateToken(UserDto user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var rsa = LoadRsaKey();

        var signingCredentials = new SigningCredentials(
            new RsaSecurityKey(rsa),
            SecurityAlgorithms.RsaSha256
            );

        var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireAt);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = CreateClaims(user),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = signingCredentials,
            Expires = expires
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string? serializedToken = tokenHandler.WriteToken(token);

        return new JwtDto
        {
            Token = serializedToken,
            ExpirationDate = expires,
        };
    }

    public string GetPublicKey()
    {
        var publicKeyContent = File.ReadAllText(_jwtOptions.RsaKey.PublicPath);
        return publicKeyContent;
    }

    private ClaimsIdentity CreateClaims(UserDto user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Username));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Role, user.RoleId.ToString()));
        
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