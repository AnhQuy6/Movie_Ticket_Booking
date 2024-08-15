using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mock_Project.Models;
using Mock_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mock_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly db_xemphimContext _xemphimContext;
        private readonly IConfiguration _configuration;

        public TaiKhoanController(db_xemphimContext xemphimContext, IConfiguration configuration)
        {
            _xemphimContext = xemphimContext;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginDTO>> Login([FromBody] LoginDTO loginDTO)
        {

            var user = await _xemphimContext.TAIKHOAN.SingleOrDefaultAsync(u => u.TenTK == loginDTO.TenTK);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.MatKhau, user.MatKhau))
            {
                return Unauthorized("Tên tài khoản hoặc mật khẩu không đúng");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.TenTK),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(50),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}
