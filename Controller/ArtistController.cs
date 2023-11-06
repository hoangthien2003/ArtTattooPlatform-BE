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
    public class ArtistController : ControllerBase
    {
        private readonly TattooPlatformEndContext _context = new TattooPlatformEndContext();
        [HttpGet("GetALL_Artist")]
        
        public IActionResult GetAll()
        {
            var artistList = _context.TblArtists.ToList();
            return Ok(artistList);
        }
        [HttpGet("GetArtistByName/{ArtistName}")]
        [Authorize(Roles = "MN, MB")]
        public IActionResult GetServiceByName([FromRoute] string artistName)
        {
            var artist = _context.TblArtists.Where(artist => artist.ArtistName == artistName).Take(5).ToList();
            if (artist == null)
            {
                return Ok("No any service matched!");
            }
            return Ok(artist);
        }

        [HttpPost("AddArtist")]
       
        public async Task<IActionResult> AddArtistAsync([FromForm] Artist artistRequest)
        {
            var existedArtist = await _context.TblArtists.
                FirstOrDefaultAsync(artist => artist.PhoneNumber == artistRequest.NumberPhone);
            if (existedArtist != null)
            {
                return BadRequest("Artist existed!");
            }
            var Artist = new TblArtist
            {
                ArtistName = artistRequest.ArtistName,
                PhoneNumber = artistRequest.NumberPhone,
                Biography = artistRequest.Biography,
                UserId = artistRequest.UserID,
                Certificate = artistRequest.Certificate,
            };
            if (artistRequest.AvatarArtist.Length > 0)
            {
                Artist.AvatarArtist = await Utils.Utils.UploadGetURLImageAsync(artistRequest.AvatarArtist);
            }
            _context.TblArtists.Add(Artist);
            await _context.SaveChangesAsync();
            return Ok(Artist);
        }
        [HttpDelete("DeleteArtist")]
        
        public async Task<IActionResult> DeleteArtistAsync(int artistID)
        {
            var artist = await _context.TblArtists.FindAsync(artistID);
            if (artist == null)
            {
                return BadRequest("Artist unavailable to delete!");
            }
            _context.TblArtists.Remove(artist);
            await _context.SaveChangesAsync();
            return Ok(artist);
        }

        [HttpPut("UpdateArtist/{artistID}")]
        
        public async Task<IActionResult> UpdateArtistAsync(int artistID, [FromForm] Artist artistRequest)
        {
            var artist = await _context.TblArtists.FindAsync(artistID);
            if (artist == null)
            {
                return BadRequest("Artist not found!");
            }

            // Cập nhật thông tin của Artist
            artist.ArtistName = artistRequest.ArtistName;
            artist.PhoneNumber = artistRequest.NumberPhone;
            artist.Biography = artistRequest.Biography;
            artist.UserId = artistRequest.UserID;
            artist.Certificate = artistRequest.Certificate;

            if (artistRequest.AvatarArtist.Length > 0)
            {
                artist.AvatarArtist = await Utils.Utils.UploadGetURLImageAsync(artistRequest.AvatarArtist);
            }

            await _context.SaveChangesAsync();
            return Ok(artist);
        }

        [HttpGet("GetArtistByID/{artistID}")]
       
        public async Task<IActionResult> GetArtistByIDAsync(int artistID)
        {
            var artist = await _context.TblArtists.FindAsync(artistID);
            if (artist == null)
            {
                return NotFound("Artist not found!");
            }

            return Ok(artist);
        }



    }
}
