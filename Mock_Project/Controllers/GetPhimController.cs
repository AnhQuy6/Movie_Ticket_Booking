using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mock_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mock_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPhimController : ControllerBase
    {
        private readonly db_xemphimContext _context;

        public GetPhimController(db_xemphimContext context)
        {
            _context = context;
        }

        // GET: api/phim  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PHIM>>> GetPhim()
        {
            return await _context.PHIM.ToListAsync();
        }

        // GET: api/phim/ 
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

        

        private bool PhimExists(int id)
        {
            return _context.PHIM.Any(e => e.MaPhim == id);
        }
    }
}