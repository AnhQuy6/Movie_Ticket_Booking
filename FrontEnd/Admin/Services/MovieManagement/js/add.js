//Thêm
const API = "https://localhost:7074/api/Phim";
document.addEventListener('DOMContentLoaded', () => {
  const form = document.querySelector('form');
  form.addEventListener('submit', async (event) => {
    event.preventDefault(); 
    // Tạo đối tượng FormData để thu thập dữ liệu từ form
    const formData = new FormData();
    formData.append('TenPhim', document.getElementById('TenPhim').value);
    formData.append('TheLoai', document.getElementById('TheLoai').value);
    formData.append('ThoiLuong', document.getElementById('ThoiLuong').value);
    formData.append('KhoiChieu', document.getElementById('KhoiChieu').value);   

    // Thu thập file ảnh
    const anhFile = document.getElementById('Anh').files[0]; 
    if (anhFile) {
      formData.append('Anh', anhFile); // Thêm ảnh vào FormData
    }
    formData.append('MoTa', document.getElementById('MoTa').value);
    try {
      // Gửi yêu cầu POST đến API
      const response = await fetch(API, {
        method: 'POST',
        body: formData, // Sử dụng FormData để gửi
      });

      if (response.ok) {
        alert('Thêm phim thành công!');
        window.location.href = 'list.html'; // Chuyển hướng về danh sách sau khi thêm
      } else {
        alert('Có lỗi xảy ra khi thêm phim.');
        console.error('Add failed:', response.status);
      }
    } catch (error) {
      console.error('Lỗi:', error);
      alert('Có lỗi xảy ra khi thêm phim.');
    }
  });
});
