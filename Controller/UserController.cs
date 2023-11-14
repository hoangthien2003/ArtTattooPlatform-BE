using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controller
{
    [Route("api/[controller]")]
    [Authorize]
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
                user.Password,
                user.UserName,
                user.Email,
                user.Image,
                user.FullName,
                user.PhoneNumber
            }).FirstOrDefaultAsync(user => user.UserId == userID);
            return Ok(userInfo);
        }

        [HttpPut("UpdateProfileUser/{userID}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userID, [FromBody] UserUpdateModel userUpdate)
        {
            var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserId == userID);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Cập nhật thông tin người dùng

            // Không được đổi email

            user.UserName = userUpdate.UserName;
            user.FullName = userUpdate.FullName;
            user.PhoneNumber = userUpdate.PhoneNumber;

            

           


            // Kiểm tra xem userUpdate.Image có giá trị mới hay không và cập nhật ảnh nếu có
            if (!string.IsNullOrEmpty(userUpdate.Image))
            {
                user.Image = userUpdate.Image;
            }

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("UpdateUsername/{userID}")]
        [Authorize]
        public async Task<IActionResult> UpdateUsernameAsync([FromRoute] int userID, [FromBody] string username)
        {
            var user = await _context.TblUsers.FindAsync(userID);
            if (user == null)
            {
                return BadRequest("Cannot find user account.");
            }
            user.UserName = username;
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("UpdatePassword/{userID}")]
        [Authorize]
        public async Task<IActionResult> UpdatePasswordAsync([FromRoute] int userID, string oldPassword, string newPassword)
        {
            var user = await _context.TblUsers.FindAsync(userID);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Lấy mật khẩu đã lưu trong cơ sở dữ liệu
            string savedPassword = user.Password;

            // Băm oldPassword và so sánh với savedPassword
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(oldPassword, savedPassword);

            if (!isPasswordCorrect)
            {
                return BadRequest("Old password incorrect!");
            }

            // Tiếp tục cập nhật mật khẩu nếu oldPassword là hợp lệ
            string hashedNewPassword = Utils.Utils.HashSaltPassword(newPassword);
            user.Password = hashedNewPassword;
            await _context.SaveChangesAsync();
            return Ok("Update password successfully!");
        }

    }
}
