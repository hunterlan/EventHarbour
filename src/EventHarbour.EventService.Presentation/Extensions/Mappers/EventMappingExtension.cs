using EventHarbour.EventService.Models;
using EventHarbour.EventService.Presentation.DTOs;

namespace EventHarbour.EventService.Presentation.Extensions.Mappers;

public static class EventMappingExtension
{
    public static EventDto ToDto(this Event source, string organizerName)
    {
        return new EventDto
        {
            Id = source.EventId,
            OrganizerFullName = organizerName,
            Title = source.Title,
            Description = source.Description,
            Price = source.Price,
            StatusName = source.Status.Name,
            CategoryName = source.Category.Name,
            VenueName = source.VenueName,
            VenueAddress = source.VenueAddress,
            City = source.City,
            Country = source.Country,
            Latitude = source.Latitude,
            Longitude = source.Longitude,
            CreatedAt = source.CreatedAt,
            UpdatedAt = source.UpdatedAt,
            Currency = source.Currency,
            MaxTickets = source.MaxTickets,
            TicketsSold = source.TicketsSold
        };
    }

    public static ShortInfoEventDto ToShortDto(this Event source)
    {
        return new ShortInfoEventDto
        {
            Id = source.EventId,
            Description = source.Description,
            Title = source.Title,
            Price = source.Price,
            StartTime = source.StartTime,
            EndTime = source.EndTime,
        };
    }
}