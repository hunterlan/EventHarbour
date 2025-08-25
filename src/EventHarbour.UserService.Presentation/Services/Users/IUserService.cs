using EventHarbour.UserService.Presentation.DTOs;

namespace EventHarbour.UserService.Presentation.Services.Users;

public interface IUserService
{
    Task<UserDto?> GetUserAsync(int id);
    Task<int> CreateUserAsync(UserDto user);
    Task UpdateUserAsync(UserDto user, int id);
    Task DeleteUserAsync(int id);
}