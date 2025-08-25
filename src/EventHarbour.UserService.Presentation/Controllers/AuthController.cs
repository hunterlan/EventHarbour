using EventHarbour.UserService.Presentation.DTOs;
using EventHarbour.UserService.Presentation.Services.Users;
using EventHarbout.UserService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventHarbour.UserService.Presentation.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    public async Task<IActionResult> Auth([FromBody] AuthDto credentials)
    {
        var user = await _userService.GetUserByLoginAsync(credentials.Login);

        //TODO: Check password by encryption.
        if (user is null || !user.Password.Equals(credentials.Password))
        {
            return Unauthorized();
        }
        
        

        throw new NotImplementedException();
    }
}