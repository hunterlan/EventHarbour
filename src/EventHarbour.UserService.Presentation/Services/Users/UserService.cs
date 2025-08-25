using EventHarbour.UserService.Presentation.DTOs;
using EventHarbour.UserService.Presentation.Extensions.Mapping;
using EventHarbour.UserService.Presentation.Helpers;
using EventHarbout.UserService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHarbour.UserService.Presentation.Services.Users;

public class UserService : IUserService
{
    private readonly UserContext _db;

    public UserService(UserContext db)
    {
        _db = db;
    }
    
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

    //TODO: Think about updating only User table?
    public async Task UpdateUserAsync(UserDto user, int id)
    {
        var foundUser = await _db.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.UserId == id);
        
        if (foundUser is null)
        {
            throw new KeyNotFoundException($"User with id {id} not found");
        }
        
        foundUser.Username = user.Username;
        foundUser.Email = user.Email;
        //TODO: Encrypt password
        foundUser.Password = user.Password;
        foundUser.Email = user.Email;
        foundUser.Profile.FirstName = user.FirstName;
        foundUser.Profile.LastName = user.LastName;
        foundUser.Profile.IsAdult = user.IsAdult;
        
        _db.Entry(foundUser).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var foundUser = await _db.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(u => u.UserId == id);
        
        if (foundUser is null)
        {
            throw new KeyNotFoundException($"User with id {id} not found");
        }
        
        _db.Users.Remove(foundUser);
        await _db.SaveChangesAsync();
    }
}