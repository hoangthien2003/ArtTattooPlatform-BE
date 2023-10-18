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
    public class ServiceController : ControllerBase
    {
        private TattooPlatformEndContext _context = new TattooPlatformEndContext();

        [HttpGet("GetAll")]
        public IActionResult GetAllService()
        {
            var serviceList = _context.TblServices.ToList();
            return Ok(serviceList);
        }

        [HttpGet("GetServiceByID/{serviceID}")]
        public async Task<IActionResult> GetServiceByIDAsync([FromRoute] int serviceID)
        {
            var service = await _context.TblServices.FindAsync(serviceID);
            if (service == null)
            {
                return BadRequest("Service not found!");
            }
            return Ok(service);
        }

        [HttpGet("v2/GetServiceByID/{serviceID}")]
        public async Task<IActionResult> GetServiceWithLogoNameByIDAsync([FromRoute] int serviceID)
        {
            var service = await _context.TblServices.FindAsync(serviceID);
            var studio = await _context.TblStudios.Select(studio => new
            {
                StudioID = studio.StudioId,
                StudioName = studio.StudioName,
                Logo = studio.Logo
            }).Where(studio => studio.StudioID == service.StudioId).FirstOrDefaultAsync();
            var result = new
            {
                service,
                studio
            };
            return Ok(result);
        }

        [HttpGet("GetServicesByName/{serviceName}")]
        public IActionResult GetServiceByName([FromRoute] string serviceName)
        {
            var service = _context.TblServices.Where(service => 
                service.ServiceName == serviceName).Take(5).ToList();
            if (service == null)
            {
                return Ok("No any service matched!");
            }
            return Ok(service);
        }

        [HttpGet("GetServiceByCategory/{categoryID}")]
        public IActionResult GetServiceByCategory([FromRoute] string categoryID)
        {
            var service = _context.TblServices.Where(service => 
                service.CategoryId == categoryID).ToList();
            if (service == null)
            {
                return Ok("No any service in this category!");
            }
            return Ok(service);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> AddServiceAsync([FromForm] Service serviceRequest)
        {
            var service = new TblService
            {
                ServiceName = serviceRequest.ServiceName,
                Description = serviceRequest.Description,
                CategoryId = serviceRequest.CategoryID,
                StudioId = serviceRequest.StudioID,
                Price = serviceRequest.Price
            };
            if (serviceRequest.Image.Length > 0)
            {
                service.ImageService = await Utils.Utils.
                    UploadGetURLImageAsync(serviceRequest.Image);
            }
            _context.TblServices.Add(service);
            _context.SaveChanges();
            return Ok(service);
        }

        [HttpPut("UpdateService/{serviceID}")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> UpdateServiceAsync([FromForm] Service serviceRequest, 
            [FromRoute] int serviceID)
        {
            var service = await _context.TblServices.FindAsync(serviceID);
            if (service == null)
            {
                return BadRequest("Service not found!");
            }
            service.ServiceName = serviceRequest.ServiceName;
            service.CategoryId = serviceRequest.CategoryID;
            service.Description = serviceRequest.Description;
            service.StudioId = serviceRequest.StudioID;
            service.Price = serviceRequest.Price;
            if (serviceRequest.Image.Length > 0)
            {
                service.ImageService = await Utils.Utils.
                    UploadGetURLImageAsync(serviceRequest.Image);
            }
            await _context.SaveChangesAsync();
            return Ok(service);
        }

        [HttpDelete("Delete/{serviceID}")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> DeleteServiceAsync([FromRoute] int serviceID)
        {
            var service = await _context.TblServices.FindAsync(serviceID);
            if (service == null)
            {
                return Ok("Service unavailable!");
            }
            _context.TblServices.Remove(service);
            _context.SaveChanges();
            return Ok(service);
        }
    }
}
