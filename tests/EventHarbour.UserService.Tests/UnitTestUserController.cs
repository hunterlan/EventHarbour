using EventHarbour.UserService.Presentation.Controllers;
using EventHarbour.UserService.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EventHarbour.UserService.Tests;

public class UnitTestUserController
{
    private readonly Mock<Presentation.Services.Users.IUserService> _userService;

    public UnitTestUserController()
    {
        _userService = new Mock<Presentation.Services.Users.IUserService>();
    }

    [Fact]
    public void Get_user_by_id()
    {
        // Arrange
        var userList = GetUsersData();
        _userService.Setup(u => u.GetUserByIdAsync(1))
            .ReturnsAsync(userList[0]);
        var userController = new UserController(_userService.Object);
        // Act
        var result = userController.Get(1).Result as OkObjectResult;
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(userList[0].Username, (result.Value as Task<UserDto>)?.Result.Username);
    }

    [Fact]
    public void Reject_creation_with_not_valid_request()
    {
        /*var userList = GetUsersData();
        _userService.Setup(u => u.CreateUserAsync(userList[1]))
            .Returns();*/
        
    }

    private List<UserDto> GetUsersData()
    {
        return new List<UserDto>()
        {
            new UserDto()
            {
                Username = "johndoe",
                Password = "StrongPass123!",
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe",
                IsAdult = true,
                RoleId = 1
            },
            new UserDto()
            {
                Username = "janesmith",
                Password = "Password456!@#",
                Email = "jane.smith@example.com",
                FirstName = "Jane",
                LastName = "Smith",
                IsAdult = true,
                RoleId = 2
            },
            new UserDto()
            {
                Username = "michaelbrown",
                Password = "Secure789$%^",
                Email = "michael.brown@example.com",
                FirstName = "Michael",
                LastName = "Brown",
                IsAdult = true,
                RoleId = 1
            },
            new UserDto()
            {
                Username = "emilywhite",
                Password = "MyPass1010&*",
                Email = "emily.white@example.com",
                FirstName = "Emily",
                LastName = "White",
                IsAdult = false,
                RoleId = 3
            },
            new UserDto()
            {
                Username = "davidlee",
                Password = "DavidPass321##",
                Email = "david.lee@example.com",
                FirstName = "David",
                LastName = "Lee",
                IsAdult = true,
                RoleId = 2
            }
        };
    }
}