using EventHarbour.UserService.Presentation.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventHarbour.UserService.Presentation.Controllers;

[Route("api/roles")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly UserContext _db;

    public RolesController(UserContext db)
    {
        _db = db;
    }
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_db.Roles);
    }
}