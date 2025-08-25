using EventHarbour.UserService.Presentation.DTOs;
using EventHarbour.UserService.Presentation.Services.JWTs;
using EventHarbour.UserService.Presentation.Services.Users;
using EventHarbout.UserService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventHarbour.UserService.Presentation.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    public AuthController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }
    [HttpPost]
    public async Task<ActionResult<JwtDto>> Auth([FromBody] AuthDto credentials)
    {
        var user = await _userService.GetUserByLoginAsync(credentials.Login);

        //TODO: Check password by encryption.
        if (user is null || !user.Password.Equals(credentials.Password))
        {
            return Unauthorized();
        }

        var token = _jwtService.GenerateToken(user);
        return Ok(token);
    }

    [HttpGet("keys")]
    public ActionResult<KeyDto> GetPublicKey()
    {
        return Ok(new KeyDto
        {
            PublicKey = _jwtService.GetPublicKey()
        });
    }
}