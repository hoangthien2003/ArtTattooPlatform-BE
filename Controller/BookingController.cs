using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.Globalization;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context;

        public BookingController()
        {
            _context = new TattooPlatformEndContext();
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _context.TblBookings
                .ToListAsync();
            return Ok(bookings);
        }

        [HttpGet("GetBookingsByStudio/{studioID}")]

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
        [Authorize]
        public async Task<IActionResult> AddBooking([FromBody] Booking bookingRequest, [FromRoute] string email)
        {
            var user = await _context.TblUsers.FirstOrDefaultAsync(user => user.Email == email);
            var booking = new TblBooking
            {
                UserId = user.UserId,
                ServiceId = bookingRequest.ServiceID,
                StudioId = bookingRequest.StudioID,
                FullName = bookingRequest.FullName,
                BookingDate = Utils.Utils.ConvertToDateTime(bookingRequest.BookingDate),
                PhoneNumber = bookingRequest.PhoneNumber,
                Total = bookingRequest.Total,
                Status = "Pending",
                Quantity = bookingRequest.Quantity
            };
            _context.TblBookings.Add(booking);
            await _context.SaveChangesAsync();
            return Ok(booking);
        }


        [HttpDelete("DeleteBooking/{bookingID}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] int bookingID)
        {
            var booking = await _context.TblBookings.FindAsync(bookingID);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            _context.TblBookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }

        [HttpGet("GetAllByUserID/{userID}")]

        public async Task<IActionResult> GetAllByMemberIDAsync([FromRoute] int userID)
        {
            var bookingList = await _context.TblBookings
                .Include(booking => booking.Service)
                .Include(booking => booking.Studio)
                .Select(booking => new
                {
                    booking.BookingDate,
                    booking.Service.ServiceName,
                    booking.Studio.StudioName,
                    booking.Total,
                    booking.UserId,
                    booking.Status,
                    booking.Service.ImageService,
                })
                .Where(booking => booking.UserId == userID)
                .ToListAsync();
            if (bookingList.Count == 0)
            {
                return Ok("List is empty");
            }
            return Ok(bookingList);
        }

        [HttpGet("GetBookingByManager/{userID}")]
        [Authorize(Roles = "MN")]
        public IActionResult GetBookingByManager([FromRoute] int userID)
        {
            var manager = _context.TblManagers.FirstOrDefault(m => m.UserId == userID);
            var booking = _context.TblBookings
                .Include(b => b.Service)
                .Include(b => b.User)
                .Select(b => new
                {
                    b.BookingId,
                    b.StudioId,
                    b.PhoneNumber,
                    b.User.UserName,
                    b.Service.ServiceName,
                    b.Status,
                    BookingDate = DateTime.Parse(b.BookingDate.ToString()).ToString("dd/MM/yyyy hh:mm tt"),
                    b.Total,
                    b.Quantity
                })
                .Where(b => b.StudioId == manager.StudioId).ToList();
            // sửa lại lấy ServiceName, Username, làm thêm api update status confirm cancel
            if (booking.Count == 0)
            {
                return BadRequest("Booking empty list.");
            }
            return Ok(booking);
        }

        [HttpPut("UpdateStatus/{bookingID}")]
        [Authorize]
        public async Task<IActionResult> UpdateStatusAsync(int bookingID, string status)
        {
            var booking = await _context.TblBookings.FirstOrDefaultAsync(b => b.BookingId == bookingID);
            if (booking == null)
            {
                return BadRequest("Not have this booking!");
            }
            booking.Status = status;
            _context.TblBookings.Update(booking);
            _context.SaveChanges();
            return Ok(booking);
        }
    }
}
