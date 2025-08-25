using EventHarbour.UserService.Presentation.DTOs;
using EventHarbour.UserService.Presentation.Extensions.Mapping;
using EventHarbour.UserService.Presentation.Helpers;
using EventHarbout.UserService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHarbour.UserService.Presentation.Services.Users;

public class UserService : IUserService
{
    private readonly UserContext _db;
    
    public async Task<UserDto?> GetUserAsync(int id)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.UserId == id);

        return user?.ToDto();
    }

    public async Task<int> CreateUserAsync(UserDto user)
    {
        var newUserRecord = new User
        {
            Username = user.Username,
            Email = user.Email,
            //TODO: Encrypt password
            Password = user.Password,
        };
        
        _db.Users.Add(newUserRecord);
        await _db.SaveChangesAsync();

        var newProfileRecord = new Profile
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsAdult = user.IsAdult,
            UserId = newUserRecord.UserId,
        };
        
        _db.Profiles.Add(newProfileRecord);
        await _db.SaveChangesAsync();
        
        return newUserRecord.UserId;
    }

    public Task UpdateUserAsync(UserDto user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }
}