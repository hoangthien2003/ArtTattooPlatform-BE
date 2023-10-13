using Azure.Core;
using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context = new TattooPlatformEndContext();

        [HttpGet("GetMemberByID/{memberID}")]
        [Authorize("MB, AT, MN")]
        public async Task<IActionResult> GetMemberByIDAsync([FromRoute] int memberID)
        {
            var member = await _context.TblMembers.FindAsync(memberID);
            if (member == null)
            {
                return BadRequest("Member not found.");
            }
            return Ok(member);
        }

        [HttpGet("GetMemberByUsername/{username}")]
        [Authorize("MB")]
        public async Task<IActionResult> GetMemberByUsernameAsync([FromRoute] string username)
        {
            var user = await _context.TblUsers.FirstOrDefaultAsync(user => user.UserName == username);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            var member = await _context.TblMembers.FirstOrDefaultAsync(member => member.UserId == user.UserId);
            var result = new
            {
                user,
                member.MemberName,
                member.PhoneNumber
            };
            return Ok(result);
        }

        [HttpPut("UpdateNamePhoneByID/{memberID}")]
        [Authorize("MB")]
        public async Task<IActionResult> UpdateProfileAsync([FromRoute] int memberID, [FromForm] Member memberRequest)
        {
            var member = await _context.TblMembers.FindAsync(memberID);
            if (member == null)
            {
                return BadRequest("Member not found.");
            }
            member.MemberName = memberRequest.MemberName;
            member.PhoneNumber = memberRequest.PhoneNumber;
            await _context.SaveChangesAsync();
            return Ok(member);
        }

        [HttpPut("UpdateUsername/{userID}")]
        [Authorize("MB")]
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
        [Authorize("MB, AT, MN")]
        public async Task<IActionResult> UpdatePasswordAsync([FromRoute] int userID, string oldPassword, string newPassword)
        {
            var user = await _context.TblUsers.FindAsync(userID); 
            if (user != null)
            {
                return BadRequest("User not found.");
            }
            string unhashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password);
            if (!oldPassword.Equals(unhashPassword))
            {
                return BadRequest("Old password incorrect!");
            }
            string hashedNewPassword = Utils.Utils.HashSaltPassword(newPassword);
            user.Password = hashedNewPassword;
            await _context.SaveChangesAsync();
            return Ok("Update password successfully!");
        }
    }
}
