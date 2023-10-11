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
        [Authorize(Roles = "MN, MB")]
        public IActionResult GetAllService()
        {
            var serviceList = _context.TblServices.ToList();
            return Ok(serviceList);
        }

        [HttpGet("GetServiceByID/{serviceID}")]
        [Authorize(Roles = "MN, MB")]
        public async Task<IActionResult> GetServiceByIDAsync([FromRoute] int serviceID)
        {
            var service = await _context.TblServices.FindAsync(serviceID);
            if (service == null)
            {
                return BadRequest("Service not found!");
            }
            return Ok(service);
        }

        [HttpGet("GetServicesByName/{serviceName}")]
        [Authorize(Roles = "MN, MB")]
        public IActionResult GetServiceByName([FromRoute] string serviceName)
        {
            var service = _context.TblServices.Where(service => service.ServiceName == serviceName).Take(5).ToList();
            if (service == null)
            {
                return Ok("No any service matched!");
            }
            return Ok(service);
        }

        [HttpGet("GetServiceByCategory/{categoryID}")]
        [Authorize(Roles = "MN, MB")]
        public IActionResult GetServiceByCategory([FromRoute] string categoryID)
        {
            var service = _context.TblServices.Where(service => service.CategoryId == categoryID).ToList();
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
            var isServiceExisted = await _context.TblServices.FindAsync(serviceRequest.ServiceID);
            if (isServiceExisted != null)
            {
                return Ok("Service existed.");
            }
            var service = new TblService
            {
                ServiceId = serviceRequest.ServiceID,
                ServiceName = serviceRequest.ServiceName,
                Description = serviceRequest.Description,
                CategoryId = serviceRequest.CategoryID,
                Price = serviceRequest.Price
            };
            if (serviceRequest.Image.Length > 0)
            {
                service.ImageService = await Utils.Utils.UploadGetURLImageAsync(serviceRequest.Image);
            }
            _context.TblServices.Add(service);
            _context.SaveChanges();
            return Ok(service);
        }

        [HttpPut("UpdateService/{serviceID}")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> UpdateServiceAsync([FromForm] Service serviceRequest, [FromRoute] int serviceID)
        {
            var service = await _context.TblServices.FindAsync(serviceID);
            if (service == null)
            {
                return BadRequest("Studio not found!");
            }
            service.ServiceName = serviceRequest.ServiceName;
            service.CategoryId = serviceRequest.CategoryID;
            service.Description = serviceRequest.Description;
            service.Price = serviceRequest.Price;
            if (serviceRequest.Image.Length > 0)
            {
                service.ImageService = await Utils.Utils.UploadGetURLImageAsync(serviceRequest.Image);
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
