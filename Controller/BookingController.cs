using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("GetBookingByID/{bookingID}")]
        [Authorize(Roles = "MB, MN")]
        public async Task<IActionResult> GetBooking([FromRoute] string id)
        {
            var booking = await _context.TblBookings
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            return Ok(booking);
        }

        [HttpPost("AddBooking")]
        [Authorize(Roles = "MB")]
        public async Task<IActionResult> AddBooking([FromBody] Booking bookingRequest)
        {
            var booking = new TblBooking
            {
                MemberId = bookingRequest.MemberID,
                ServiceId = bookingRequest.ServiceID,
                StudioId = bookingRequest.StudioID,
                BookingDate = bookingRequest.BookingDate,
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
