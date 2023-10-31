using back_end.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controller
{
    [Route("api/[controller]")]
    [Authorize(Roles = "MB, AT, MN")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context = new TattooPlatformEndContext();

        [HttpGet("GetUserInfoByUserID/{userID}")]
        public async Task<IActionResult> GetUserInfoAsync([FromRoute] int userID)
        {
            var userInfo = await _context.TblUsers.Select(user => new
            {
                user.UserId,
                user.UserName,
                user.Email,
                user.Image,
                user.FullName,
                user.PhoneNumber
            }).FirstOrDefaultAsync(user => user.UserId == userID);
            return Ok(userInfo);
        }
    }
}
