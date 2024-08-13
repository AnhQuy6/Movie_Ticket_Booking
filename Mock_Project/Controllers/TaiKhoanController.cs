using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mock_Project.Models;

namespace Mock_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly db_xemphimContext _xemphimContext;
        public TaiKhoanController(db_xemphimContext xemphimContext)
        {
            _xemphimContext = xemphimContext;
        }
        [HttpPost]
        [Route("Register")]

        public ActionResult<TAIKHOAN> Register(TAIKHOAN userAccount)
        {
            if (_xemphimContext.TAIKHOAN.Any(u => u.TenTK == userAccount.TenTK || u.Email == userAccount.Email || u.SDT == userAccount.SDT)) {
                return BadRequest("Tên tài khoản hoặc email hoặc số điện thoại đã tồn tại");
            }
            _xemphimContext.TAIKHOAN.Add(userAccount);
            _xemphimContext.SaveChanges();
            return Ok(userAccount);
        }
    }
}
