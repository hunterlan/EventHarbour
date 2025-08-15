using EventHarbour.EventService.Models;
using EventHarbour.EventService.Presentation.DTOs;

namespace EventHarbour.EventService.Presentation.Services;

public interface IEventService
{
    IEnumerable<ShortInfoEventDto> GetEvents();
    Task<EventDto?> GetEventAsync(int id, CancellationToken token);
    Task<Event> AddEventAsync(CreateEventDto newEvent, CancellationToken token);
    Task<Event> UpdateEventAsync(int eventId, CreateEventDto updatedEvent, CancellationToken token);
    Task DeleteEventAsync(int id, CancellationToken token);
}