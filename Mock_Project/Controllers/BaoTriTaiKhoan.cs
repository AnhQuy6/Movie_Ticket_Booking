using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mock_Project.Models;
using System;

namespace Mock_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaoTriTaiKhoan : ControllerBase
    {
        private readonly db_xemphimContext _context;

        public BaoTriTaiKhoan(db_xemphimContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<TAIKHOAN> GetTaiKhoans()
        {
            var taiKhoans = _context.TAIKHOAN.ToList();
            return Ok(taiKhoans);
        }

        [HttpGet("{id}")]
        public ActionResult<TAIKHOAN> GetTaiKhoan(int id)
        {
            var taiKhoan = _context.TAIKHOAN.Find(id);

            if (taiKhoan == null)
            {
                return NotFound();
            }
            return Ok(taiKhoan);
        }

        [HttpPost]
        public ActionResult<TAIKHOAN> CreateTaiKhoan(TAIKHOAN userAccount)
        {
            if (_context.TAIKHOAN.Any(u => u.TenTK == userAccount.TenTK || u.Email == userAccount.Email || u.SDT == userAccount.SDT))
            {
                return BadRequest("Tên tài khoản hoặc email hoặc số điện thoại đã tồn tại");
            }

            _context.TAIKHOAN.Add(userAccount);
            _context.SaveChanges();

            return Ok(userAccount);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTaiKhoan(int id, TAIKHOAN taiKhoan)
        {
            var existingTaiKhoan = _context.TAIKHOAN.FirstOrDefault(tk => tk.MaTK == id);
            if (existingTaiKhoan == null)
            {
                return NotFound();
            }

            existingTaiKhoan.TenTK = taiKhoan.TenTK != null ? taiKhoan.TenTK : existingTaiKhoan.TenTK;
            existingTaiKhoan.MatKhau = taiKhoan.MatKhau != null ? taiKhoan.MatKhau : existingTaiKhoan.MatKhau;
            existingTaiKhoan.TenNguoiDung = taiKhoan.TenNguoiDung != null ? taiKhoan.TenNguoiDung : existingTaiKhoan.TenNguoiDung;
            existingTaiKhoan.NgaySinh = taiKhoan.NgaySinh != null ? taiKhoan.NgaySinh : existingTaiKhoan.NgaySinh;
            existingTaiKhoan.DiaChi = taiKhoan.DiaChi != null ? taiKhoan.DiaChi : existingTaiKhoan.DiaChi;
            existingTaiKhoan.SDT = taiKhoan.SDT != null ? taiKhoan.SDT : existingTaiKhoan.SDT;
            existingTaiKhoan.Email = taiKhoan.Email != null ? taiKhoan.Email : existingTaiKhoan.Email;

            try
            {
                _context.SaveChanges();
                return Ok(taiKhoan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaiKhoan(int id)
        {
            var taiKhoan = _context.TAIKHOAN.Find(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            _context.TAIKHOAN.Remove(taiKhoan);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
