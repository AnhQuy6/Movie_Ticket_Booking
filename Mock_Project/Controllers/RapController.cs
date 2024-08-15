using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mock_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mock_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RAPController : ControllerBase
    {
        private readonly db_xemphimContext _context;

        public RAPController(db_xemphimContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RAP>>> GetRaps()
        {
            return await _context.RAP.ToListAsync();
        }

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
    }
}