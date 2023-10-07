using back_end.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace back_end.Controller
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private TattooThuBayContext _context = new TattooThuBayContext();

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<TblUser>> Register(RegisterRequest request)
        {
            bool isExistEmail = await _context.TblUsers.AnyAsync(u => u.Email == request.Email);
            if (isExistEmail)
            {
                return BadRequest("auth/existed-email");
            }
            bool isExistUsername = await _context.TblUsers.AnyAsync(u => u.Username == request.Username);
            if (isExistUsername)
            {
                return BadRequest("auth/existed-username");
            }
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);
            TblUser user = new()
            {
                Username = request.Username,
                Email = request.Email,
                Password = hashedPassword,
                Status = true,
                CreateUser = DateTime.UtcNow,
                RoleId = "MB"
            };
            _context.TblUsers.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest request)
        {
            TblUser? user = await _context.TblUsers.FirstOrDefaultAsync(row => row.Email == request.Email);
            if (user == null)
            {
                return BadRequest("auth/incorrect-email");
            }
            if (!VerifyPasswordHash(request.Password, user.Password))
            {
                return BadRequest("auth/incorrect-password");
            }
            string token = CreateToken(user);
            return Ok(token);
        }

        private static bool VerifyPasswordHash(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        private string CreateToken(TblUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleId),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
