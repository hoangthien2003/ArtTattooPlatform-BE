using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context = new TattooPlatformEndContext();
        [HttpGet("GetALL_Feedback")]
        public IActionResult GetAll()
        {
            var feedBackList = _context.TblFeedbacks.ToList();
            return Ok(feedBackList);
        }
        [HttpPost("AddFeedback")]
        public async Task<IActionResult> AddFeedbackAsync([FromForm] FeedbackRequest feedbackRequest)
        {

            var FeedBacK = new TblFeedback
            {
                FeedbackDetail = feedbackRequest.FeedbackDetail,
                MemberId = feedbackRequest.MemberId,
                ServiceId = feedbackRequest.ServiceId,
                FeedbackDate = feedbackRequest.FeedbackDate,
            };
            _context.TblFeedbacks.Add(FeedBacK);
            await _context.SaveChangesAsync();
            return Ok(FeedBacK);
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
        [HttpPut("UpdateFeedBack/{FeedBackID}")]
        public async Task<IActionResult> UpdateFeedBackAsync(int feedbackID, [FromForm] FeedbackRequest feedBackRequest)
        {
            var feedback = await _context.TblFeedbacks.FindAsync(feedbackID);
            if (feedback == null)
            {
                return BadRequest("Feedback not found!");
            }

            // Cập nhật feedBack
            feedback.FeedbackDetail = feedBackRequest.FeedbackDetail;
            feedback.MemberId = feedBackRequest?.MemberId;
            feedback.ServiceId = feedBackRequest?.ServiceId;
            feedback.FeedbackDate = feedBackRequest?.FeedbackDate;

            await _context.SaveChangesAsync();
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
    }
}
