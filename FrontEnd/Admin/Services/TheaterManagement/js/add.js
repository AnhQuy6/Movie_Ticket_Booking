//Thêm
const API = "https://localhost:7074/api/Rap";
document.addEventListener('DOMContentLoaded', () => {
  const form = document.querySelector('form');
  form.addEventListener('submit', async (event) => {
    event.preventDefault(); 
    // Thu thập dữ liệu từ form
    const tenRap = document.getElementById('tenRap').value;
    const diaChi = document.getElementById('diaChi').value;
    const anh = document.getElementById('anh').value;
    const sdt = document.getElementById('sdt').value;
    const newRap = {
      tenRap: tenRap,
      diaChi: diaChi,
      anh: anh,
      sdt: sdt
    };
    try {
      // Gửi yêu cầu POST đến API
      const response = await fetch(API, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newRap),
      });
      if (response.ok) {
        alert('Thêm rạp phim thành công!');
        window.location.href = 'list.html'; // Chuyển hướng về danh sách sau khi thêm
      } else {
        alert('Có lỗi xảy ra khi thêm rạp phim.');
        console.error('Add failed:', response.status);
      }
    } catch (error) {
      console.error('Lỗi:', error);
      alert('Có lỗi xảy ra khi thêm rạp phim.');
    }
  });
});