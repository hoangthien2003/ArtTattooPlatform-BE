using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controller
{
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("GetMessage"), Authorize(Roles = "MB")]
        public IActionResult GetMessage()
        {
            return Ok("You are TattooLover!");
        }

        [HttpGet("GetMember"), Authorize(Roles = "MN")]
        public IActionResult GetMember()
        {
            return Ok("You are Manager!");
        }
    }
}
