using EventHarbour.UserService.Presentation.DTOs;
using EventHarbout.UserService.Models;

namespace EventHarbour.UserService.Presentation.Extensions.Mapping;

public static class UserMappingExtension
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            FirstName = user.Profile.FirstName,
            LastName = user.Profile.LastName,
            IsAdult = user.Profile.IsAdult,
            RoleId = user.Role.RoleId
        };
    }
}