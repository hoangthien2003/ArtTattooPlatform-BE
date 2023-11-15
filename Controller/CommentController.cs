using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace back_end.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly TattooPlatformEndContext _context = new TattooPlatformEndContext();
        [HttpPost("CommentFeedback/{feedbackId}")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> CommentFeedback([FromRoute] int feedbackId, [FromBody] Comment comment)
        {
            var feedback = await _context.TblFeedbacks.FindAsync(feedbackId);
            if (feedback == null)
            {
                return BadRequest("Feedback not found!");
            }

            // Thêm comment vào bảng Comments hoặc bất kỳ cơ chế lưu trữ nào bạn đang sử dụng
            var newComment = new TblComment
            {
                FeedbackId = feedbackId,
                CommentDetail = comment.CommentDetail
            };

            _context.TblComments.Add(newComment);
            await _context.SaveChangesAsync();

            return Ok(newComment);
        }

        [HttpDelete("DeleteComment/{commentId}")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
        {
            var comment = await _context.TblComments.FindAsync(commentId);
            if (comment == null)
            {
                return NotFound("Comment not found.");
            }

            _context.TblComments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }
        [HttpPut("UpdateComment/{commentId}")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> UpdateComment([FromRoute] int commentId, [FromBody] Comment comment)
        {
            var existingComment = await _context.TblComments.FindAsync(commentId);
            if (existingComment == null)
            {
                return NotFound("Comment not found.");
            }

            // Cập nhật comment
            existingComment.CommentDetail = comment.CommentDetail;

            await _context.SaveChangesAsync();

            return Ok(existingComment);
        }
    }
}
