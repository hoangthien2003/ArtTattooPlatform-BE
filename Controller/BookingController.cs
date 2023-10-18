using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context;

        public BookingController(TattooPlatformEndContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "MB, MN")]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _context.TblBookings
                .ToListAsync();
            return Ok(bookings);
        }

        [HttpPost("AddBooking/{email}")]
        [Authorize(Roles = "MB, MN")]
        public async Task<IActionResult> AddBooking([FromBody] Booking bookingRequest, [FromRoute] string email)
        {
            var user = await _context.TblUsers.Where(user => user.Email == email).FirstOrDefaultAsync();
            var member = await _context.TblMembers.
                Where(member => member.UserId == user.UserId).FirstOrDefaultAsync();
            DateTime dateTime = DateTime.ParseExact(bookingRequest.BookingDate, 
                "M/d/yyyy h:mm tt", CultureInfo.InvariantCulture);
            var booking = new TblBooking
            {
                BookingId = System.Guid.NewGuid().ToString(),
                MemberId = member.MemberId,
                ServiceId = bookingRequest.ServiceID,
                StudioId = bookingRequest.StudioID,
                BookingDate = dateTime,
                PhoneNumber = bookingRequest.PhoneNumber,
                Total = bookingRequest.Total
            };

            _context.TblBookings.Add(booking);
            await _context.SaveChangesAsync();
            return Ok(booking);
        }

        [HttpDelete("DeleteBooking/{deleteID}")]
        [Authorize(Roles = "MB")]
        public async Task<IActionResult> DeleteBooking([FromRoute] int id)
        {
            var booking = await _context.TblBookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            _context.TblBookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }
    }
}
