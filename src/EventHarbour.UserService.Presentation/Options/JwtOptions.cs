using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace EventHarbour.UserService.Presentation.Options;

public class JwtOptions
{
    public const string Jwt = "Jwt";
    
    [Required]
    public string Issuer { get; init; } = string.Empty;
    [Required]
    public string Audience { get; init; } = string.Empty;
    [Required]
    public required int ExpireAt { get; init; }
    [ValidateObjectMembers] 
    public RsaKeyOptions RsaKey { get; init; } = null!;
}