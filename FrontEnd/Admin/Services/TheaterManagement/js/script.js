const API = "https://localhost:7074/api/Rap";
//Load data
document.addEventListener("DOMContentLoaded", loadRapData);
async function loadRapData() {
  try {
    const response = await fetch(API);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data = await response.json();
    const tbody = document.querySelector("tbody");
    tbody.innerHTML = " ";
    data.forEach((rap) => {
      const tr = document.createElement("tr");
      tr.innerHTML = `
        <td>${rap.maRap}</td>
        <td>${rap.tenRap}</td>
        <td>${rap.diaChi}</td>
        <td><img src="${rap.anh}" alt="Ảnh rạp phim" class="img-thumbnail" style="max-width: 100px;"></td>
        <td>${rap.sdt}</td>
        <td>
          <a href="edit.html?id=${rap.maRap}" class="btn btn-warning btn-sm">Sửa</a>
          <button class="btn btn-danger btn-sm" onclick="deleteRap(${rap.maRap}); return false;">Xóa</button>
        </td>
      `;
      tbody.appendChild(tr);
    });
  } catch (error) {
    console.error("Error fetching data:", error);
  }
}
// xóa rạp phim
async function deleteRap(id) {
  if (confirm("Bạn có chắc chắn muốn xóa không?")) {
    try {
      const response = await fetch(`${API}/${id}`, {
        method: "DELETE",
      });

      if (response.ok) {
        alert("Xóa thành công!");
        loadRapData();
      } else {
        alert("Xóa thất bại! Vui lòng thử lại.");
        console.error("Failed to delete:", response.status);
      }
    } catch (error) {
      alert("Đã xảy ra lỗi trong quá trình xóa.");
      console.error("Error deleting data:", error);
    }
  }
}
//Thêm
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