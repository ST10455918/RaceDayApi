//Part 3

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayApi.Data;
using RaceDayApi.Helpers;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayContext _ctx;
        public ResultsController(RaceDayContext ctx) => _ctx = ctx;

        [HttpPost]
        public async Task<IActionResult> AddResult([FromBody] Result res)
        {
            if (!SessionHelper.IsOrganiser(HttpContext)) return Forbid();
            _ctx.Results.Add(res);
            await _ctx.SaveChangesAsync();
            return Ok(res);
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetByEvent(int eventId)
        {
            var results = await _ctx.Results.Include(r => r.Enrolment).ThenInclude(e => e!.Category)
               .Where(r => r.Enrolment!.Category!.EventId == eventId).ToListAsync();
            return Ok(results);
        }
    }
}