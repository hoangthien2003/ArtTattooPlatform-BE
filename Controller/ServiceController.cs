using back_end.Entities;
using back_end.Models;
using back_end.Services;
using Microsoft.AspNetCore.Authorization;
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
            var serviceList = _context.TblServices.Include(s => s.Studio).Select(s => new
            {
                s.ServiceId,
                s.ServiceName,
                s.Description,
                s.Price,
                s.CategoryId,
                s.ImageService,
                s.ArtistId,
                s.StudioId,
                s.Rating,
                s.Studio
            }).ToList();
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

        [HttpGet("v3/GetServiceByID/{serviceID}")]
        public async Task<IActionResult> GetServiceByIDV3Async([FromRoute] int serviceID)
        {
            var existingService = await _context.TblServices.Include(s => s.Studio).Select(s => new
            {
                s.ServiceId,
                s.ServiceName,
                s.Description,
                s.Price,
                s.CategoryId,
                s.ImageService,
                s.ArtistId,
                s.StudioId,
                s.Rating,
                s.Studio
            }).FirstOrDefaultAsync(s => s.ServiceId == serviceID);
            if (existingService == null)
            {
                return BadRequest("Not have anything service with this ID.");
            }
            return Ok(existingService);
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
        [HttpGet("GetServiceByStudio/{studioID}")]
        public IActionResult GetServiceByStudio([FromRoute] int studioID)
        {
            var services = _context.TblServices.Where(service => service.StudioId == studioID).ToList();

            if (services.Count == 0)
            {
                return Ok("No services found for this studio.");
            }

            return Ok(services);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> AddServiceAsync(Service serviceRequest)
        {
            var service = new TblService
            {
                ServiceName = serviceRequest.ServiceName,
                Description = serviceRequest.Description,
                StudioId = serviceRequest.StudioID,
                Price = serviceRequest.Price,
                ImageService = serviceRequest.Image,
                Rating = 0
            };
            _context.TblServices.Add(service);
            _context.SaveChanges();
            return Ok(service);
        }

      



        [HttpPut("UpdateService/{serviceID}")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> UpdateServiceAsync([FromRoute] int serviceID, Service serviceRequest)
        {
            var service = await _context.TblServices.FindAsync(serviceID);
            if (service == null)
            {
                return BadRequest("Service not found!");
            }
            service.ServiceName = serviceRequest.ServiceName;
            service.Description = serviceRequest.Description;
            service.StudioId = serviceRequest.StudioID;
            service.Price = serviceRequest.Price;
            service.ImageService = serviceRequest.Image;
            _context.TblServices.Update(service);
            await _context.SaveChangesAsync();
            return Ok(service);
        }

        [HttpGet("UpdateAverageRatingForService/{serviceID}")]
     
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
        public IActionResult GetTopRatedServices()
        {
            try
            {
                var topRatedServices = _context.TblServices.Include(s => s.Studio).Select(s => new { s.ServiceId, s.ServiceName, s.Description, s.Price, s.CategoryId, s.ImageService, s.ArtistId, s.StudioId, s.Rating, s.Studio })
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

     

        [HttpGet("NewestServices")]
        public IActionResult GetNewestServices()
        {
            try
            {
                var newestServices = _context.TblServices
                    .Include(s => s.Studio)
                    .Select(s => new
                    {
                        s.ServiceId,
                        s.ServiceName,
                        s.Description,
                        s.Price,
                        s.CategoryId,
                        s.ImageService,
                        s.ArtistId,
                        s.StudioId,
                        s.Rating,
                        s.Studio
                    })
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



        [HttpGet("FeedbackInfo/{serviceID}")]
        public IActionResult GetFeedbackInfo([FromRoute] int serviceID)
        {
            try
            {
                // Lấy tất cả đánh giá cho dịch vụ cụ thể
                var feedbackList = _context.TblFeedbacks
                    .Where(feedback => feedback.ServiceId == serviceID)
                    .ToList();

                // Khởi tạo một dictionary để lưu trữ số lượng đánh giá cho từng xếp hạng
                var ratingStats = new Dictionary<int, int>();

                // Khởi tạo số lượng đánh giá cho mỗi xếp hạng từ 1 đến 5
                for (int i = 1; i <= 5; i++)
                {
                    ratingStats[i] = 0;
                }

                // Đếm số lượng đánh giá cho từng xếp hạng
                foreach (var feedback in feedbackList)
                {
                    if (feedback.Rating.HasValue && feedback.Rating >= 1 && feedback.Rating <= 5)
                    {
                        int rating = feedback.Rating.Value;
                        ratingStats[rating]++;
                    }
                }

                // Tính tổng số lượng đánh giá
                var totalFeedbackCount = feedbackList.Count;

                var feedbackInfo = new
                {
                    TotalFeedbackCount = totalFeedbackCount,
                    RatingStats = ratingStats
                };

                // Trả về kết quả số liệu thống kê (tổng số lượng đánh giá và số lượng đánh giá cho từng xếp hạng)
                return Ok(feedbackInfo);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi trong quá trình lấy số liệu thống kê đánh giá.");
            }
        }
    }
}
