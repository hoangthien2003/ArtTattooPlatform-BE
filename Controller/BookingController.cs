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

        [HttpGet("GetBookingsByStudio/{studioID}")]
        [Authorize(Roles = " MN")]
        public IActionResult GetBookingsByStudio([FromRoute] int studioID)
        {
            var bookings = _context.TblBookings.Where(booking => booking.StudioId == studioID).ToList();

            if (bookings.Count == 0)
            {
                return Ok("No bookings found for this studio.");
            }

            return Ok(bookings);
        }

        [HttpGet("GetBookingsByService/{serviceID}")]
        [Authorize(Roles = " MN")]
        public IActionResult GetBookingsByService([FromRoute] int serviceID)
        {
            var bookings = _context.TblBookings.Where(booking => booking.ServiceId == serviceID).ToList();

            if (bookings.Count == 0)
            {
                return Ok("No bookings found for this service.");
            }

            return Ok(bookings);
        }

        [HttpGet("GetBookingByID/{bookingID}")]
        [Authorize(Roles = "MB, MN")]
        public async Task<IActionResult> GetBookingByID([FromRoute] string bookingID)
        {
            var booking = await _context.TblBookings.FindAsync(bookingID);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            return Ok(booking);
        }

        [HttpPost("AddBooking/{email}")]
        [Authorize(Roles = "MB")]

        public async Task<IActionResult> AddBooking([FromBody] Booking bookingRequest, [FromRoute] string email)
        {
            var user = await _context.TblUsers.Where(user => user.Email == email).FirstOrDefaultAsync();
            var member = await _context.TblMembers.
                Where(member => member.UserId == user.UserId).FirstOrDefaultAsync();
            
                
            var booking = new TblBooking
            {
                BookingId = System.Guid.NewGuid().ToString(),
                MemberId = member.MemberId,
                ServiceId = bookingRequest.ServiceID,
                StudioId = bookingRequest.StudioID,
                BookingDate = DateTime.UtcNow,
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
