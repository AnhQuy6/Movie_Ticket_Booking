using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mock_Project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mock_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RapController : ControllerBase
    {
        private readonly db_xemphimContext _context;

        public RapController(db_xemphimContext context)
        {
            _context = context;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RAP>>> GetRaps()
        {
            return await _context.RAP.ToListAsync();
        }

        // GET: 
        [HttpGet("{id}")]
        public async Task<ActionResult<RAP>> GetRap(int id)
        {
            var rap = await _context.RAP.FindAsync(id);

            if (rap == null)
            {
                return NotFound();
            }

            return rap;
        }

        // PUT:
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRap(int id, RAP rap)
        {
            if (id != rap.MaRap)
            {
                return BadRequest(new { message = "ID không khớp với MaRap" });
            }
            _context.Entry(rap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RapExists(id))
                {
                    return NotFound(new { message = "Không tìm thấy rạp với ID này" });
                }
                else
                {
                    throw;
                }
            }
            return Ok(rap);
        }

        // POST: 
        [HttpPost]
        public async Task<ActionResult<RAP>> PostRap(RAP rap)
        {
            _context.RAP.Add(rap);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRap), new { id = rap.MaRap }, rap);
        }

        // DELETE: 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRap(int id)
        {
            var rap = await _context.RAP.FindAsync(id);
            if (rap == null)
            {
                return NotFound();
            }

            _context.RAP.Remove(rap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RapExists(int id)
        {
            return _context.RAP.Any(e => e.MaRap == id);
        }
    }
}
