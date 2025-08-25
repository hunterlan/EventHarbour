using EventHarbour.UserService.Presentation.DTOs;

namespace EventHarbour.UserService.Presentation.Services.Users;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByLoginAsync(string login);
    Task<int> CreateUserAsync(UserDto user);
    Task UpdateUserAsync(UserDto user, int id);
    Task DeleteUserAsync(int id);
}