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
        public async Task<ActionResult<RAP>> PutRap(int id, [FromForm] IFormFile anh, [FromForm] string tenRap, [FromForm] string diaChi, [FromForm] string soDienThoai)
        {
            var rap = await _context.RAP.FindAsync(id);

            if (rap == null)
            {
                return NotFound();
            }

            rap.TenRap = tenRap;
            rap.DiaChi = diaChi;
            rap.SDT = soDienThoai;

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
                rap.Anh = "/images/" + newFileName;
            }

            _context.Entry(rap).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: 
        //[HttpPost]
        //public async Task<ActionResult<RAP>> PostRap(RAP rap)
        //{
        //    _context.RAP.Add(rap);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetRap), new { id = rap.MaRap }, rap);
        //}
        //UPLOAD FILE
        [HttpPost]
        public async Task<ActionResult<RAP_IMAGE>> PostFile([FromForm] RAP_IMAGE rapImage)
        {

            var rap = new RAP
            {
                TenRap = rapImage.TenRap,
                DiaChi = rapImage.DiaChi,
                SDT = rapImage.SDT,
            };
            if (rapImage.Anh != null && rapImage.Anh.Length > 0)
            {

                var fileName = Path.GetFileNameWithoutExtension(rapImage.Anh.FileName);
                var extension = Path.GetExtension(rapImage.Anh.FileName);


                var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", newFileName);

                // Lưu file ảnh vào đường dẫn đã chỉ định
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await rapImage.Anh.CopyToAsync(stream);
                }
                // Lưu đường dẫn ảnh vào cơ sở dữ liệu (đường dẫn tương đối)
                rap.Anh = "/images/" + newFileName;
            }


            _context.RAP.Add(rap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRap", new { id = rap.MaRap }, rap);
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
