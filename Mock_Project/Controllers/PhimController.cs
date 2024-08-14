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
        public async Task<ActionResult<PHIM>> PutPhim(int id, [FromForm] IFormFile anh, [FromForm] string tenPhim, [FromForm] string theLoai, [FromForm] string thoiLuong, [FromForm] TimeOnly khoiChieu, [FromForm] string moTa)
        {
            var phim = await _context.PHIM.FindAsync(id);

            if (phim == null)
            {
                return NotFound();
            }
            phim.TenPhim = tenPhim;
            phim.TheLoai = theLoai;
            phim.ThoiLuong = thoiLuong;
            phim.KhoiChieu = khoiChieu;
            phim.MoTa = moTa;

            if (anh != null && anh.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(anh.FileName);
                var extension = Path.GetExtension(anh.FileName);
                var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", newFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await anh.CopyToAsync(stream);
                }
                phim.Anh = "/images/" + newFileName;
            }

            _context.Entry(phim).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // POST
        //[HttpPost]
        //public async Task<ActionResult<PHIM>> PostPhim(PHIM phim)
        //{
        //    _context.PHIM.Add(phim);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPhim", new { id = phim.MaPhim }, phim);
        //}
        //UPLOAD FILE
        [HttpPost]
        public async Task<ActionResult<PHIM>> PostFile([FromForm] PHIM_IMAGE phimImage)
        {
            var phim = new PHIM
            {
                TenPhim = phimImage.TenPhim,
                TheLoai = phimImage.TheLoai,
                ThoiLuong = phimImage.ThoiLuong,
                KhoiChieu = phimImage.KhoiChieu,
                MoTa = phimImage.MoTa,
            };

            if (phimImage.Anh != null && phimImage.Anh.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(phimImage.Anh.FileName);
                var extension = Path.GetExtension(phimImage.Anh.FileName);
                var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", newFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await phimImage.Anh.CopyToAsync(stream);
                }
                phim.Anh = "/images/" + newFileName;
            }
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
