using System.ComponentModel.DataAnnotations;

namespace EventHarbour.EventService.Presentation.DTOs;

public class EventDto : ShortInfoEventDto
{
    public string OrganizerFullName { get; set; }
    public string CategoryName { get; set; }
    [MaxLength(150)]
    public string VenueName { get; set; }
    public string VenueAddress { get; set; }
    [MaxLength(100)]
    public string City { get; set; }
    [MaxLength(100)]
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    [MaxLength(3)]
    public string Currency { get; set; }
    public int MaxTickets { get; set; }
    public int TicketsSold { get; set; }

    public string StatusName { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}