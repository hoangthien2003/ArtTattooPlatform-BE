using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace back_end.Controller
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context = new TattooPlatformEndContext();

        [HttpGet("GetALL_Feedback")]
        
        public async Task<IActionResult> GetFeedback()
        {
            var feedback = await _context.TblFeedbacks
                .ToListAsync();
            return Ok(feedback);
        }

        [HttpGet("GetFeedbackByID/{FeedbackID}")]
        
        public async Task<IActionResult> GetFeedBackByIDAsync(int FeedbackID)
        {
            var feedback = await _context.TblFeedbacks.FindAsync(FeedbackID);
            if (feedback == null)
            {
                return NotFound("Feedback not found!");
            }

            return Ok(feedback);
        }



        [HttpPost("AddFeedback")]
        public async Task<IActionResult> AddFeedback([FromBody] Feedback feedbackRequest)
        {
            var Feedback0 = new TblFeedback
            {  
                FeedbackDetail = feedbackRequest.FeedbackDetail,
                UserId = feedbackRequest.UserID,
                ServiceId = feedbackRequest.ServiceID,
                Rating = feedbackRequest.Rating,
                FeedbackDate =DateTime.UtcNow
            };
           _context.TblFeedbacks.Add(Feedback0);
           await _context.SaveChangesAsync();
            return Ok(Feedback0);
        }

        [HttpDelete("DeleteFeedback")]
        
        public async Task<IActionResult> DeleteFeedbackAsync(int feedbackID)
        {
            var feedback = await _context.TblFeedbacks.FindAsync(feedbackID);
            if (feedback == null)
            {
                return BadRequest("Feedback unavailable to delete!");
            }
            _context.TblFeedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return Ok(feedback);
        }

        [HttpPut("UpdateFeedback/{feedbackID}")]
       
        public async Task<IActionResult> UpdateFeedback([FromRoute] int feedbackID, [FromBody] Feedback feedbackRequest)
        {
            var feedback = await _context.TblFeedbacks.FindAsync(feedbackID);

            if (feedback == null)
            {
                return NotFound("Không tìm thấy đánh giá.");
            }
            // Cập nhật thông tin đánh giá
            feedback.FeedbackDetail = feedbackRequest.FeedbackDetail;
            feedback.UserId = feedbackRequest.UserID;
            feedback.ServiceId = feedbackRequest.ServiceID;
            feedback.Rating = feedbackRequest.Rating;
            feedback.FeedbackDate = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(feedback);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Lỗi trong quá trình cập nhật đánh giá.");
            }
        }

        

        [HttpGet("GetAverageRatingForService/{serviceID}")]
       
        public IActionResult GetAverageRatingForService([FromRoute] int serviceID)
        {
            // Lấy trung bình đánh giá sao cho dịch vụ
            var averageRating = _context.TblFeedbacks
            .Where(feedback => feedback.ServiceId == serviceID)
            .Average(feedback => feedback.Rating);
            return Ok(averageRating);
        }

        [HttpGet("GetFeedbackByServiceID/{ServiceID}")]
        public async Task<IActionResult> GetFeedbackByServiceIDAsync([FromRoute] int ServiceID)
        {
            var feedBack = await _context.TblFeedbacks
                .Include(f => f.User)
                .Select(f => new
                {
                    f.FeedbackId,
                    f.FeedbackDetail,
                    f.FeedbackDate,
                    f.ServiceId,
                    f.Rating,
                    f.User.UserName,
                    f.User
                })
                .Where(s => s.ServiceId == ServiceID).ToListAsync();
            if (feedBack == null)
            {
                return NotFound("Feed not found!");
            }
            return Ok(feedBack);
        }
    }
}
