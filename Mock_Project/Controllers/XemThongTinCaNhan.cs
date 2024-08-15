using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mock_Project.Models;
using System.Linq;

namespace Mock_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XemThongTinCaNhan : ControllerBase
    {
        private readonly db_xemphimContext _context;

        public XemThongTinCaNhan(db_xemphimContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetTaiKhoanById(int id)
        {
            var taiKhoan = _context.TAIKHOAN.FirstOrDefault(tk => tk.MaTK == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            taiKhoan.MatKhau = null;
            return Ok(taiKhoan);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTaiKhoan(int id, [FromBody] TAIKHOAN updatedTaiKhoan)
        {
            var taiKhoan = _context.TAIKHOAN.FirstOrDefault(tk => tk.MaTK == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            taiKhoan.TenTK = updatedTaiKhoan.TenTK != null ? updatedTaiKhoan.TenTK : taiKhoan.TenTK;
            taiKhoan.MatKhau = updatedTaiKhoan.MatKhau != null ? updatedTaiKhoan.MatKhau : taiKhoan.MatKhau;
            taiKhoan.TenNguoiDung = updatedTaiKhoan.TenNguoiDung != null ? updatedTaiKhoan.TenNguoiDung : taiKhoan.TenNguoiDung;
            taiKhoan.NgaySinh = updatedTaiKhoan.NgaySinh != null ? updatedTaiKhoan.NgaySinh : taiKhoan.NgaySinh;
            taiKhoan.DiaChi = updatedTaiKhoan.DiaChi != null ? updatedTaiKhoan.DiaChi : taiKhoan.DiaChi;
            taiKhoan.SDT = updatedTaiKhoan.SDT != null ? updatedTaiKhoan.SDT : taiKhoan.SDT;
            taiKhoan.Email = updatedTaiKhoan.Email != null ? updatedTaiKhoan.Email : taiKhoan.Email;

            _context.SaveChanges();

            return Ok(taiKhoan);
        }
    }
}
