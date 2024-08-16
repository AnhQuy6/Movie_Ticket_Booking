using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mock_Project.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mock_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeController : ControllerBase
    {
        private readonly db_xemphimContext _context;

        public VeController(db_xemphimContext context)
        {
            _context = context;
        }

        // Lấy danh sách rạp
        [HttpGet("cinemas")]
        public async Task<IActionResult> GetCinemas()
        {
            var cinemas = await _context.RAP.ToListAsync();
            return Ok(cinemas);
        }

        // Lấy danh sách phòng chiếu theo rạp
        [HttpGet("rooms/{cinemaId}")]
        public async Task<IActionResult> GetRooms(int cinemaId)
        {
            var rooms = await _context.PHONG
                .Where(r => r.MaRap == cinemaId)
                .ToListAsync();
            return Ok(rooms);
        }

        // Lấy danh sách suất chiếu theo phòng chiếu
        [HttpGet("showtimes/{roomId}")]
        public async Task<IActionResult> GetShowtimes(int roomId)
        {
            var showtimes = await _context.SUATCHIEU
                .Where(s => s.MaPhong == roomId)
                .ToListAsync();
            return Ok(showtimes);
        }

        // Lấy danh sách ghế đã đặt theo suất chiếu
        [HttpGet("seats/{showtimeId}")]
        public async Task<IActionResult> GetSeats(int showtimeId)
        {
            var bookedSeats = await _context.VEPHIM
                .Where(v => v.MaSuatChieu == showtimeId)
                .Select(v => v.ViTri)
                .ToListAsync();

            return Ok(bookedSeats);
        }

        // Tạo vé mới
        [HttpPost("tickets")]
        public async Task<IActionResult> CreateTicket([FromBody] VEPHIM ticket)
        {
            if (ticket == null || ticket.ViTri == null)
            {
                return BadRequest("Dữ liệu vé không hợp lệ.");
            }

            if (string.IsNullOrEmpty(ticket.MaTK.ToString()))
            {
                return BadRequest("Mã tài khoản không hợp lệ.");
            }

            // Kiểm tra xem ghế đã được đặt trước đó chưa
            var existingTickets = await _context.VEPHIM
                .Where(v => v.MaSuatChieu == ticket.MaSuatChieu && v.ViTri == ticket.ViTri)
                .ToListAsync();

            if (existingTickets.Any())
            {
                // Nếu ghế đã được đặt, trả về thông báo lỗi
                return BadRequest("Ghế đã được đặt trước đó.");
            }

            // Lưu vé mới vào cơ sở dữ liệu
            _context.VEPHIM.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

    }
}
