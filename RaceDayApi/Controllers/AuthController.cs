using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayApi.Data;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RaceDayContext _ctx;
        public AuthController(RaceDayContext ctx) => _ctx = ctx;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User dto)
        {
            if (await _ctx.Users.AnyAsync(u => u.Username == dto.Username)) return BadRequest("Username taken");
            dto.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
            if (dto.Role != "Organiser") dto.Role = "Participant";
            _ctx.Users.Add(dto);
            await _ctx.SaveChangesAsync();
            return Ok(new { dto.Id, dto.Username, dto.Role });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Username == login.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash)) return Unauthorized("Invalid login");
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("Username", user.Username);
            return Ok(new { user.Id, user.Username, user.Role });
        }

        [HttpPost("logout")]
        public IActionResult Logout() { HttpContext.Session.Clear(); return Ok("Logged out"); }
    }
    public class LoginDto { public string Username { get; set; } = ""; public string Password { get; set; } = ""; }
}