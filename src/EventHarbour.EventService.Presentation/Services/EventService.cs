using EventHarbour.EventService.Models;
using EventHarbour.EventService.Presentation.DTOs;
using EventHarbour.EventService.Presentation.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EventHarbour.EventService.Presentation.Services;

public class EventService : IEventService
{
    private readonly EventContext _db;

    public IEnumerable<ShortInfoEventDto> GetEvents()
    {
        return _db.Events.Select(e => new ShortInfoEventDto
        {
            Id = e.EventId,
            Description = e.Description,
            Title = e.Title,
            Price = e.Price,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
        }).AsEnumerable();
    }

    public async Task<EventDto?> GetEventAsync(int id, CancellationToken token)
    {
        var @event = await _db.Events
            .Include(e => e.Category)
            .Include(e => e.Status)
            .FirstOrDefaultAsync(e => e.EventId == id, cancellationToken: token);

        if (@event == null)
        {
            return null;
        }
        
        // Making query to user service...

        return new EventDto()
        {
            Id = @event.EventId,
            OrganizerFullName = "",
            Title = @event.Title,
            Description = @event.Description,
            Price = @event.Price,
            StatusName = @event.Status.Name,
            CategoryName = @event.Category.Name,
            VenueName = @event.VenueName,
            VenueAddress = @event.VenueAddress,
            City = @event.City,
            Country = @event.Country,
            Latitude = @event.Latitude,
            Longitude = @event.Longitude,
            CreatedAt = @event.CreatedAt,
            UpdatedAt = @event.UpdatedAt,
            Currency = @event.Currency,
            MaxTickets = @event.MaxTickets,
            TicketsSold = @event.TicketsSold
        };
    }

    public async Task<Event> AddEvent(CreateEventDto newEvent, CancellationToken token)
    {
        // Making query to user service...
        // if (isOrganizerExists == null)


        var isCategoryExists = await _db.Categories.AnyAsync(c => c.CategoryId == newEvent.CategoryId, cancellationToken: token);
        if (isCategoryExists == false)
        {
            throw new KeyNotFoundException($"Category ID {newEvent.CategoryId} is not presented in DB.");
        }
        
        var isStatusExists = await _db.Statuses.AnyAsync(s => s.StatusId == newEvent.StatusId, cancellationToken: token);
        if (isStatusExists == false)
        {
            throw new KeyNotFoundException($"Status ID {newEvent.StatusId} is not presented in DB.");
        }

        var @event = new Event
        {
            OrganizerId = newEvent.OrganizerId,
            Title = newEvent.Title,
            Description = newEvent.Description,
            Price = newEvent.Price,
            StatusId = newEvent.StatusId,
            CategoryId = newEvent.CategoryId,
            StartTime = newEvent.StartTime,
            EndTime = newEvent.EndTime,
            VenueName = newEvent.VenueName,
            VenueAddress = newEvent.VenueAddress,
            City = newEvent.City,
            Country = newEvent.Country,
            Latitude = newEvent.Latitude,
            Longitude = newEvent.Longitude,
            Currency = newEvent.Currency,
            MaxTickets = newEvent.MaxTickets,
            TicketsSold = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
        };
        _db.Events.Add(@event);
        await _db.SaveChangesAsync(token);
        
        return @event;
    }
}