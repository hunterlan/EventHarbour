using EventHarbour.EventService.Models;
using EventHarbour.EventService.Presentation.DTOs;
using EventHarbour.EventService.Presentation.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventHarbour.EventService.Presentation.Controllers;

[Route("api/events")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ShortInfoEventDto>> Get()
    {
        return Ok(_eventService.GetEvents());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> Get(int id, CancellationToken cancellationToken)
    {
        return Ok(await _eventService.GetEventAsync(id, cancellationToken));
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Event>> Post([FromBody] CreateEventDto newEventDto, CancellationToken cancellationToken)
    {
        try
        {
            var addEvent = await _eventService.AddEvent(newEventDto, cancellationToken);
            return Created("api/events", addEvent);
        }
        catch (KeyNotFoundException kEx)
        {
            return BadRequest(kEx.Message);
        }
    }
    
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] CreateEventDto newEventDto, CancellationToken cancellationToken)
    {
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _eventService.DeleteEventAsync(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException kEx)
        {
            return NotFound();
        }
    }
}