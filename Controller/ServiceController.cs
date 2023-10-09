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

        [HttpPost("Add")]
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

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteServiceAsync(int serviceID)
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
