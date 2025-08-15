using EventHarbour.EventService.Models;
using EventHarbour.EventService.Presentation.DTOs;
using EventHarbour.EventService.Presentation.Extensions.Mappers;
using EventHarbour.EventService.Presentation.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EventHarbour.EventService.Presentation.Services;

public class EventService : IEventService
{
    private readonly EventContext _db;

    public IEnumerable<ShortInfoEventDto> GetEvents() => _db.Events.Select(e => e.ToShortDto()).AsEnumerable();

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

        return @event.ToDto();
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

    public async Task<Event> UpdateEventAsync(int eventId, CreateEventDto updatedEvent, CancellationToken token)
    {
        var eventForUpdate = await _db.Events.FindAsync([eventId], cancellationToken: token);
        if (eventForUpdate is null)
        {
            throw new KeyNotFoundException($"Event ID {eventId} is not presented in DB.");
        }

        if (eventForUpdate.TicketsSold > updatedEvent.MaxTickets)
        {
            throw new ArgumentException("Max tickets can't be lower than sold tickets.");
        }

        eventForUpdate.OrganizerId = updatedEvent.OrganizerId;
        eventForUpdate.Title = updatedEvent.Title;
        eventForUpdate.Description = updatedEvent.Description;
        eventForUpdate.CategoryId = updatedEvent.CategoryId;
        eventForUpdate.StartTime = updatedEvent.StartTime;
        eventForUpdate.EndTime = updatedEvent.EndTime;
        eventForUpdate.VenueName = updatedEvent.VenueName;
        eventForUpdate.VenueAddress = updatedEvent.VenueAddress;
        eventForUpdate.City = updatedEvent.City;
        eventForUpdate.Country = updatedEvent.Country;
        eventForUpdate.Latitude = updatedEvent.Latitude;
        eventForUpdate.Longitude = updatedEvent.Longitude;
        eventForUpdate.Price = updatedEvent.Price;
        eventForUpdate.Currency = updatedEvent.Currency;
        eventForUpdate.MaxTickets = updatedEvent.MaxTickets;
        eventForUpdate.StatusId = updatedEvent.StatusId;
        eventForUpdate.UpdatedAt = DateTime.Now;
        
        _db.Entry(eventForUpdate).State = EntityState.Modified;
        await _db.SaveChangesAsync(token);
        
        return eventForUpdate;
    }

    public Task DeleteEventAsync(int id, CancellationToken token)
    {
        var @event = _db.Events.FirstOrDefault(e => e.EventId == id);
        if (@event == null)
        {
            throw new KeyNotFoundException($"Event with ID {id} is not presented in DB.");
        }
        
        _db.Events.Remove(@event);
        return _db.SaveChangesAsync(token);
    }
}