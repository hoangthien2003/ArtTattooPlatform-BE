using back_end.Entities;
using back_end.Hubs;
using back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context;
        private readonly IHubContext<MessageHub> _hubContext;

        public BookingController(IHubContext<MessageHub> hubContext)
        {
            _context = new TattooPlatformEndContext();
            _hubContext = hubContext;
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

        [HttpPost("AddBooking/{phoneNumber}")]
        public async Task<IActionResult> AddBooking([FromBody] Booking bookingRequest, [FromRoute] string phoneNumber)
        {
            var member = _context.TblUsers.FirstOrDefault(m => m.PhoneNumber == phoneNumber);
            var booking = new TblBooking();
            if (member == null)
            {
                booking = new TblBooking
                {
                    BookingId = System.Guid.NewGuid().ToString(),
                    ServiceId = bookingRequest.ServiceID,
                    StudioId = bookingRequest.StudioID,
                    BookingDate = Utils.Utils.ConvertToDateTime(bookingRequest.BookingDate),
                    PhoneNumber = bookingRequest.PhoneNumber,
                    Total = bookingRequest.Total,
                    Status = "Pending"
                };
            }
            else booking = new TblBooking
            {
                BookingId = System.Guid.NewGuid().ToString(),
                UserId = member.UserId,
                ServiceId = bookingRequest.ServiceID,
                StudioId = bookingRequest.StudioID,
                BookingDate = Utils.Utils.ConvertToDateTime(bookingRequest.BookingDate),
                PhoneNumber = bookingRequest.PhoneNumber,
                Total = bookingRequest.Total,
                Status = "Pending"
            };
            
            _context.TblBookings.Add(booking);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("BookingService", booking);
            return Ok(booking);
        }

        [HttpDelete("DeleteBooking/{deleteID}")]
       
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
    }
}
