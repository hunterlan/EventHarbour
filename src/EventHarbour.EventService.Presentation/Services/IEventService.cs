using EventHarbour.EventService.Models;
using EventHarbour.EventService.Presentation.DTOs;

namespace EventHarbour.EventService.Presentation.Services;

public interface IEventService
{
    IEnumerable<ShortInfoEventDto> GetEvents();
    Task<EventDto?> GetEventAsync(int id, CancellationToken token);
    Task<Event> AddEvent(CreateEventDto newEvent, CancellationToken token);
    Task DeleteEventAsync(int id, CancellationToken token);
}