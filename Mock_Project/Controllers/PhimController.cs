using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mock_Project.Models;

namespace Mock_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        private readonly db_xemphimContext _context;

        public PhimController(db_xemphimContext context)
        {
            _context = context;
        }

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PHIM>>> GetPhim()
        {
            return await _context.PHIM.ToListAsync();
        }
        // GET: 
        [HttpGet("{id}")]
        public async Task<ActionResult<PHIM>> GetPhim(int id)
        {
            var phim = await _context.PHIM.FindAsync(id);

            if (phim == null)
            {
                return NotFound();
            }

            return phim;
        }
        // PUT:
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhim(int id, PHIM phim)
        {
            if (id != phim.MaPhim)
            {
                return BadRequest(new { message = "ID không khớp với MaPhim" });
            }
            _context.Entry(phim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhimExists(id))
                {
                    return NotFound(new { message = "Không tìm thấy phim với ID này" });
                }
                else
                {
                    throw;
                }
            }
            return Ok(phim); 
        }
        // POST
        [HttpPost]
        public async Task<ActionResult<PHIM>> PostPhim(PHIM phim)
        {
            _context.PHIM.Add(phim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhim", new { id = phim.MaPhim }, phim);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhim(int id)
        {
            var phim = await _context.PHIM.FindAsync(id);
            if (phim == null)
            {
                return NotFound();
            }

            _context.PHIM.Remove(phim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhimExists(int id)
        {
            return _context.PHIM.Any(e => e.MaPhim == id);
        }
    }
}
