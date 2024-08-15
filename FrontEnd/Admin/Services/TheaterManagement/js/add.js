//Thêm
const API = "https://localhost:7074/api/Rap";
document.addEventListener('DOMContentLoaded', () => {
  const form = document.querySelector('form');
  form.addEventListener('submit', async (event) => {
    event.preventDefault(); 
    // Tạo đối tượng FormData để thu thập dữ liệu từ form
    const formData = new FormData();
    formData.append('tenRap', document.getElementById('tenRap').value);
    formData.append('diaChi', document.getElementById('diaChi').value);
    
    // Thu thập file ảnh
    const anhFile = document.getElementById('anh').files[0]; 
    if (anhFile) {
      formData.append('anh', anhFile); // Thêm ảnh vào FormData
    }
    formData.append('sdt', document.getElementById('sdt').value);
    try {
      // Gửi yêu cầu POST đến API
      const response = await fetch(API, {
        method: 'POST',
        body: formData, // Sử dụng FormData để gửi
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
