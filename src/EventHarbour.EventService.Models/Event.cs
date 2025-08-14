using System.ComponentModel.DataAnnotations;

namespace EventHarbour.EventService.Models;

public class Event
{
    public int EventId { get; set; }
    public int OrganizerId { get; set; }
    [MaxLength(150)]
    public string Title { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    [MaxLength(150)]
    public string VenueName { get; set; }
    public string VenueAddress { get; set; }
    [MaxLength(100)]
    public string City { get; set; }
    [MaxLength(100)]
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public decimal Price { get; set; }
    [MaxLength(3)]
    public string Currency { get; set; }
    [MinLength(1)]
    public int MaxTickets { get; set; }
    public int TicketsSold { get; set; }

    public int StatusId { get; set; }
    public Status Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}