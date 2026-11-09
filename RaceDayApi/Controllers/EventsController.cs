// Uses RaceDayContext
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayApi.Data;
using RaceDayApi.Models;
using RaceDayApi.Services;

namespace RaceDayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayContext _context;
        public EventsController(RaceDayContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Events.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return Ok(ev);
        }

        [HttpPost("{id}/upload-banner")]
        public async Task<IActionResult> UploadBanner(int id, IFormFile file, [FromServices] BlobService blobService)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            ev.BannerImageUrl = await blobService.UploadAsync(file);
            await _context.SaveChangesAsync();
            return Ok(new { imageUrl = ev.BannerImageUrl });
        }
    }
}