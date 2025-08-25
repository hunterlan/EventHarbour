using System.ComponentModel.DataAnnotations;

namespace EventHarbour.UserService.Presentation.Options;

public class RsaKeyOptions
{
    [Required]
    public string PrivatePath { get; init; } = string.Empty;
    [Required]
    public string PublicPath { get; init; } = string.Empty;
}