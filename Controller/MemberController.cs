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
    [Authorize(Roles = "MB")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context = new TattooPlatformEndContext();

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var members = await _context.TblMembers.ToListAsync();
            return Ok(members);
        }

        [HttpGet("GetMemberByID/{userID}")]
        
        public async Task<IActionResult> GetMemberByIDAsync([FromRoute] int userID)
        {
            var member = await _context.TblMembers.Include(member => member.User).FirstOrDefaultAsync(
                member => member.UserId == userID
            );
            if (member == null)
            {
                return BadRequest("Member not found.");
            }
            return Ok(member);
        }

        [HttpGet("GetMemberByUsername/{username}")]
        
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
        [Authorize(Roles = "MB")]
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

        


        [HttpDelete("DeleteMember/{userID}")]
        
        public async Task<IActionResult> DeleteMemberAsync([FromRoute] int userID)
        {
            var member = await _context.TblMembers.FirstOrDefaultAsync(m => m.UserId == userID);
            if (member == null)
            {
                return BadRequest("Cannot find member account.");
            }

            _context.TblMembers.Remove(member);
            await _context.SaveChangesAsync();

            return Ok(member);
        }
    }
}
