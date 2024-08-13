using Microsoft.AspNetCore.Mvc;
using Mock_Project.Models;
using BCrypt.Net;
using Mock_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mock_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly db_xemphimContext _xemphimContext;

        public RegisterController(db_xemphimContext xemphimContext)
        {
            _xemphimContext = xemphimContext;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<RegisterDTO>> Register([FromBody] RegisterDTO registerDTO)
        {
            if (await _xemphimContext.TAIKHOAN.AnyAsync(u => u.TenTK == registerDTO.TenTK || u.Email == registerDTO.Email || u.SDT == registerDTO.SDT))
            {
                return BadRequest("Tên tài khoản hoặc email hoặc số điện thoại đã tồn tại");
            }

            var userAccount = new TAIKHOAN
            {
                TenTK = registerDTO.TenTK,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(registerDTO.MatKhau),
                DiaChi = registerDTO.DiaChi,
                NgaySinh = registerDTO.NgaySinh,
                Email = registerDTO.Email,
                SDT = registerDTO.SDT,
                TenNguoiDung = registerDTO.TenNguoiDung
            };

            _xemphimContext.TAIKHOAN.Add(userAccount);
            await _xemphimContext.SaveChangesAsync();

            return Ok(userAccount);
        }
    }
}
