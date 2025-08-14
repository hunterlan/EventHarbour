using EventHarbour.EventService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHarbour.EventService.Presentation.Helpers;

public class EventContext(DbContextOptions<EventContext> options) : DbContext(options)
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Status> Statuses { get; set; }
}