using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context = new TattooPlatformEndContext();

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var studioList = _context.TblStudios.ToList();
            return Ok(studioList);
        }

        [HttpPost("AddStudio")]
        public async Task<IActionResult> AddStudioAsync([FromForm] Studio studioRequest)
        {
            var existedStudio = await _context.TblStudios.
                FirstOrDefaultAsync(studio => studio.Address == studioRequest.Address);
            if (existedStudio != null)
            {
                return BadRequest("Studio existed!");
            }
            var studio = new TblStudio
            {
                StudioName = studioRequest.StudioName,
                Address = studioRequest.Address,
                StudioPhone = studioRequest.StudioPhone,
                StudioEmail = studioRequest.StudioEmail,
                ManagerId = studioRequest.ManagerID,
                Description = studioRequest.Description
            };
            if (studioRequest.Avatar.Length > 0)
            {
                studio.Avatar = await Utils.Utils.UploadGetURLImageAsync(studioRequest.Avatar);
            }
            _context.TblStudios.Add(studio);
            await _context.SaveChangesAsync();
            return Ok(studio);
        }

        [HttpDelete("DeleteStudio")]
        public async Task<IActionResult> DeleteStudioAsync(int studioID)
        {
            var studio = await _context.TblStudios.FindAsync(studioID);
            if (studio == null)
            {
                return BadRequest("Studio unavailable to delete!");
            }
            _context.TblStudios.Remove(studio);
            await _context.SaveChangesAsync();
            return Ok(studio);
        }
    }
}
