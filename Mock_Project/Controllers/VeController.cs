using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mock_Project.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Mock_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeController : ControllerBase
    {
        private readonly db_xemphimContext _context;

        public VeController(db_xemphimContext context)
        {
            _context = context;
        }

        [HttpGet("cinemas")]
        public async Task<IActionResult> GetCinemas()
        {
            var cinemas = await _context.RAP.ToListAsync();
            return Ok(cinemas);
        }

        [HttpGet("rooms/{cinemaId}")]
        public async Task<IActionResult> GetRooms(int cinemaId)
        {
            var rooms = await _context.PHONG
                .Where(r => r.MaRap == cinemaId)
                .ToListAsync();
            return Ok(rooms);
        }

        [HttpGet("showtimes/{roomId}")]
        public async Task<IActionResult> GetShowtimes(int roomId)
        {
            var showtimes = await _context.SUATCHIEU
                .Where(s => s.MaPhong == roomId)
                .ToListAsync();
            return Ok(showtimes);
        }

        [HttpPost("tickets")]
        public async Task<IActionResult> CreateTicket([FromBody] VEPHIM ticket)
        {
            if (ticket == null)
            {
                return BadRequest("Invalid ticket data.");
            }

            _context.VEPHIM.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }
    }
}
