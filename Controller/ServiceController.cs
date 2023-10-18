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
        [HttpGet("UpdateAverageRatingForService/{serviceID}")]
        [Authorize(Roles = "MN,MB")]
        public IActionResult UpdateAverageRatingForService([FromRoute] int serviceID)
        {
            var feedbacks = _context.TblFeedbacks.Where(feedback => feedback.ServiceId == serviceID).ToList();

            if (feedbacks.Count > 0)
            {
                double averageRating = (double)feedbacks.Average(feedback => feedback.Rating);

                var service = _context.TblServices.FirstOrDefault(s => s.ServiceId == serviceID);

                if (service != null)
                {
                    service.Rating = (int)averageRating;
                    _context.SaveChanges();
                    return Ok("Average rating updated successfully.");
                }
                else
                {
                    return NotFound("Service not found.");
                }
            }
            else
            {
                return Ok("No feedback available for this service.");
            }
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

        [HttpGet("TopRatedServices")]
        [Authorize(Roles = "MN")]
        public IActionResult GetTopRatedServices()
        {
            try
            {
                var topRatedServices = _context.TblServices
                    .OrderByDescending(service => service.Rating) // Sắp xếp theo xếp hạng giảm dần
                    .Take(10) // Lấy 10 dịch vụ có xếp hạng cao nhất (số lượng có thể điều chỉnh)
                    .ToList();

                return Ok(topRatedServices);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi trong quá trình lấy danh sách top-rated services.");
            }
        }

        //[HttpGet("NewServices")]
        //public IActionResult GetNewServices()
        //{
        //try
        //{
        // Định nghĩa thời gian tối đa để xem dịch vụ là mới.
        //var maxNewServiceAge = DateTime.UtcNow.AddMonths(-1); // Ví dụ: Lấy dịch vụ mới trong vòng 1 tháng (số tháng có thể điều chỉnh).

        //var newServices = _context.TblServices
        //.Where(service => service.CreatedAt >= maxNewServiceAge) // Lọc các dịch vụ mới
        //.ToList();

        //return Ok(newServices);
        //}
        //catch (Exception ex)
        //{
        //return BadRequest("Lỗi trong quá trình lấy danh sách new services.");
        //}
        // }

        [HttpGet("NewestServices")]
        [Authorize(Roles = "MN")]
        public IActionResult GetNewestServices()
        {
            try
            {
                var newestServices = _context.TblServices
                    .OrderByDescending(service => service.ServiceId) // Sắp xếp giảm dần theo ServiceID
                    .Take(5) // Lấy 5 dịch vụ mới nhất
                    .ToList();

                return Ok(newestServices);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi trong quá trình lấy danh sách dịch vụ mới nhất.");
            }



        }
    }
 }
