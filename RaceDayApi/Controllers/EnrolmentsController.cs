using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayApi.Data;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrolmentsController : ControllerBase
    {
        private readonly RaceDayContext _context;
        public EnrolmentsController(RaceDayContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Enrolments.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Enrolment enrolment)
        {
            _context.Enrolments.Add(enrolment);
            await _context.SaveChangesAsync();
            return Ok(enrolment);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var list = await _context.Enrolments.Where(e => e.UserId == userId).ToListAsync();
            return Ok(list);
        }
    }
}