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
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context = new TattooPlatformEndContext();

        [HttpGet("GetALL_Feedback")]
        [Authorize(Roles = "MN, AT, MB")]
        public IActionResult GetAll()
        {
            var feedBackList = _context.TblFeedbacks.ToList();
            return Ok(feedBackList);
        }

        [HttpPost("AddFeedback")]
        [Authorize(Roles = "MB")]


        public async Task<IActionResult> AddServiceAsync([FromForm] Feedback feedbackRequest)
        {
            
            var Feedback1 = new TblFeedback
            {  
                FeedbackDetail = feedbackRequest.FeedbackDetail,
                MemberID = feedbackRequest.MemberID,
                ServiceID = feedbackRequest.ServiceID,
                FeedbackDate = feedbackRequest.FeedbackDate,
                
                
            };
            
            _context.TblFeedbacks.Add(Feedback1);
            _context.SaveChanges();
            return Ok(Feedback1);
        }

        [HttpDelete("DeleteFeedback")]
        [Authorize(Roles = "MB")]
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

        [HttpPut("UpdateFeedBack/{FeedBackID}")]
        [Authorize(Roles = "MB")]
        public async Task<IActionResult> UpdateFeedBackAsync(int feedbackID, [FromForm] Feedback feedBackRequest)
        {
            var feedback = await _context.TblFeedbacks.FindAsync(feedbackID);
            if (feedback == null)
            {
                return BadRequest("Feedback not found!");
            }

            // Cập nhật feedBack
            feedback.FeedbackDetail = feedBackRequest.FeedbackDetail;
            feedback.MemberID = feedBackRequest?.MemberID;
            feedback.ServiceID = feedBackRequest?.ServiceID;
            feedback.FeedbackDate = feedBackRequest?.FeedbackDate;

            await _context.SaveChangesAsync();
            return Ok(feedback);
        }

        [HttpGet("GetFeedbackByID/{FeedbackID}")]
        [Authorize(Roles = "MN, AT, MB")]
        public async Task<IActionResult> GetFeedBackByIDAsync(int FeedbackID)
        {
            var feedback = await _context.TblFeedbacks.FindAsync(FeedbackID);
            if (feedback == null)
            {
                return NotFound("Feedback not found!");
            }

            return Ok(feedback);
        }

        [HttpPost("AddRating")]
        [Authorize(Roles = "MB")]
        public async Task<IActionResult> AddFeedBackAsync([FromBody] Feedback feedbackRequest)
        {
            // Kiểm tra xem dịch vụ có tồn tại hay không
            var feedback = await _context.TblFeedbacks.FindAsync(feedbackRequest.FeedbackID);
            if (feedback == null)
            {
                return BadRequest("Feedback not found!");
            }
            // Tạo đánh giá sao mới
            var feedback1 = new TblFeedback
            {
                ServiceID = feedbackRequest.ServiceID,
                Rating = feedbackRequest.Rating,
                // Thêm các thông tin khác liên quan đến đánh giá sao nếu cần
            };

            _context.TblFeedbacks.Add(feedback1);
            await _context.SaveChangesAsync();

            return Ok(feedback1);
        }
        
        [HttpGet("GetAverageRatingForService/{serviceID}")]
        [Authorize(Roles = "MN")]
        public IActionResult GetAverageRatingForService([FromRoute] int serviceID)
        {
            // Lấy trung bình đánh giá sao cho dịch vụ
            var averageRating = _context.TblFeedbacks
            .Where(feedback => feedback.ServiceID == serviceID)
            .Average(feedback => feedback.Rating);

            return Ok(averageRating);
        }
    }
}
