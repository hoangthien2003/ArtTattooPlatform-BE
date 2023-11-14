using back_end.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controller
{
    [Route("api/[controller]")]
    [Authorize(Roles = "MN")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private TattooPlatformEndContext _context;
        public ManagerController(TattooPlatformEndContext context)
        {
            _context = context;
        }

        [HttpGet("GetBookingOverview/{userID}")]
        public async Task<IActionResult> GetBookingOverviewAsync([FromRoute] int userID)
        {
            var manager = await _context.TblManagers.FirstOrDefaultAsync(m => m.UserId == userID);
            DateTime startDay = DateTime.Today.AddDays((int)(DateTime.Today.DayOfWeek - DayOfWeek.Monday));
            DateTime endOfDay = DateTime.Today.AddDays(1).AddTicks(-1);
            var bookingData = _context.TblBookings
            .Where(b => b.StudioId == manager.StudioId && b.BookingDate >= startDay && b.BookingDate <= endOfDay)
            .GroupBy(b => b.BookingDate.Value.Date)
            .Select(group => new
            {
                Date = group.Key.ToString("dd/MM"),
                BookingTotal = group.Count()
            }).ToList();
            return Ok(bookingData);
        }
    }
}
