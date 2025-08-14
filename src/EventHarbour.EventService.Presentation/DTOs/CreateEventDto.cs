using System.ComponentModel.DataAnnotations;

namespace EventHarbour.EventService.Presentation.DTOs;

public class CreateEventDto
{
    [Required]
    public int OrganizerId { get; set; }
    [Required]
    [MaxLength(150)]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    [MaxLength(150)]
    public string VenueName { get; set; }
    [Required]
    public string VenueAddress { get; set; }
    [Required]
    [MaxLength(100)]
    public string City { get; set; }
    [Required]
    [MaxLength(100)]
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    [MaxLength(3)]
    public string Currency { get; set; }
    [Required]
    [MinLength(1)]
    public int MaxTickets { get; set; }
    [Required]
    public int StatusId { get; set; }
}