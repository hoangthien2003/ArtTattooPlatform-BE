using back_end.Entities;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _context.TblBookings
                .Include(b => b.Member)
                .Include(b => b.TblBookingDetails)
                .ToListAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _context.TblBookings
                .Include(b => b.Member)
                .Include(b => b.TblBookingDetails)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking bookingRequest)
        {
            var isBookingExisted = await _context.TblBookings.FindAsync(bookingRequest.BookingId);
            if (isBookingExisted != null)
            {
                return Ok("Booking existed.");
            }

            var bookings = new TblBooking
            {
                MemberId = bookingRequest.MemberId,
                BookingId = bookingRequest.BookingId,
                BookingDate = bookingRequest.BookingDate,
                StudioId = bookingRequest.StudioId,
            };

            _context.TblBookings.Add(bookings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = bookingRequest.BookingId }, bookingRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest("Invalid booking ID.");
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound("Booking not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
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

        private bool BookingExists(int id)
        {
            return _context.TblBookings.Any(e => e.BookingId == id);
        }
    }
}
