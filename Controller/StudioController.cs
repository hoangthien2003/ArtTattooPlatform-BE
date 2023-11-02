﻿using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        [HttpGet("GetStudioByID/{studioID}")]
        
        public async Task<IActionResult> GetStudioByIDAsync([FromRoute] int studioID)
        {
            var studio = await _context.TblStudios.FindAsync(studioID);
            if (studio == null)
            {
                return BadRequest("Studio is unavailable!");
            }
            return Ok(studio);
        }

        [HttpGet("GetStudioByName/{studioName}")]
        
        public IActionResult GetServiceByName([FromRoute] string studioName)
        {
            var studio = _context.TblStudios.Where(studio =>
                studio.StudioName == studioName).Take(5).ToList();
            if (studio == null)
            {
                return Ok("No any service matched!");
            }
            return Ok(studio);
        }

        [HttpGet("GetStudioByManager/{managerID}")]
        
        public async Task<IActionResult> GetStudioByManagerAsync([FromRoute] int managerID)
        {
            var studios = await _context.TblStudios
                .Where(studios => studios.ManagerId == managerID)
                .ToListAsync();
            if (studios.Count == 0)
                return BadRequest("The manager not have any studio!");

            return Ok(studios);
        }

        [HttpGet("GetLogoNameByID/{studioID}")]
       
        public async Task<IActionResult> GetLogoNameByIDAsync([FromRoute] int studioID)
        {
            var result = await _context.TblStudios.Select(studio => new
            {
                StudioID = studio.StudioId,
                StudioName = studio.StudioName,
                Logo = studio.Logo
            }).Where(studio => studio.StudioID == studioID).FirstOrDefaultAsync();
            return Ok(result);
        }

        [HttpPost("AddStudio")]
        [Authorize(Roles = "MN")]
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
            if (studioRequest.Logo.Length > 0)
            {
                studio.Logo = await Utils.Utils.UploadGetURLImageAsync(studioRequest.Logo);
            }
            _context.TblStudios.Add(studio);
            await _context.SaveChangesAsync();
            return Ok(studio);
        }

        [HttpPut("UpdateStudio/{studioID}")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> UpdateStudioAsync([FromForm] Studio studioRequest, [FromRoute] int studioID)
        {
            var studio = await _context.TblStudios.FindAsync(studioID);
            if (studio == null)
            {
                return BadRequest("Studio not found!");
            }
            studio.StudioName = studioRequest.StudioName;
            studio.Address = studioRequest.Address;
            studio.StudioPhone = studioRequest.StudioPhone;
            studio.StudioEmail = studioRequest.StudioEmail;
            studio.Description = studioRequest.Description;
            if (studioRequest.Logo.Length > 0)
            {
                studio.Logo = await Utils.Utils.UploadGetURLImageAsync(studioRequest.Logo);
            }
            await _context.SaveChangesAsync();
            return Ok(studio);
        }

        [HttpDelete("DeleteStudio/{studioID}")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> DeleteStudioAsync([FromRoute] int studioID)
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

        [HttpGet("TopRatedStudios/{studioID}")]
        [Authorize(Roles = "MN,MB")]
        public IActionResult GetTopRatedStudios()
        {
            // Đây là nơi bạn sẽ truy vấn cơ sở dữ liệu để lấy danh sách các studio có top rating.
            // Sắp xếp và chọn studio dựa trên rating và lựa chọn số lượng studio bạn muốn hiển thị.

            var topRatedStudios = _context.TblStudios
                .OrderByDescending(studio => studio.RatingStb)
                .Take(5) // Thay đổi số lượng studio bạn muốn hiển thị ở đây
                .ToList();

            return Ok(topRatedStudios);
        }
    }
}
