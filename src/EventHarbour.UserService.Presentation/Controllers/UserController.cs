using EventHarbour.UserService.Presentation.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventHarbour.UserService.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDto newUser)
        {
            throw new NotImplementedException();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto updatedUser)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
