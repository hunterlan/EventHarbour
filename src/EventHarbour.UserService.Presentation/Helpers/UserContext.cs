using EventHarbout.UserService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHarbour.UserService.Presentation.Helpers;

public class UserContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>()
            .HasKey(p => p.UserId)
            .HasName("profile_pkey");

        modelBuilder.Entity<Profile>()
            .HasOne(p => p.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<Profile>(p => p.UserId);
    }
}