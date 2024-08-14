const API = "https://localhost:7074/api/Phim";
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
    data.forEach((phim) => {
      const tr = document.createElement("tr");
      tr.innerHTML = `
        <td>${phim.maPhim}</td>
        <td>${phim.tenPhim}</td>
        <td>${phim.theLoai}</td>
        <td>${phim.thoiLuong}</td>
        <td>${phim.khoiChieu}</td>
        <td>
          <img src="https://localhost:7074${phim.anh}" alt="Ảnh phim" class="img-thumbnail" style="max-width: 100px;">
        </td>
        <td>${phim.moTa}</td>
        <td>
          <a href="edit.html?id=${phim.maPhim}" class="btn btn-warning btn-sm">Sửa</a>
          <button class="btn btn-danger btn-sm" onclick="deleteRap(${phim.maPhim}); return false;">Xóa</button>
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
        loadRapData(); // Tải lại dữ liệu sau khi xóa
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
//Sua
// Load old data
async function loadData(id) {
  try {
    const response = await fetch(`${API}/${id}`);
    if (response.ok) {
      const data = await response.json();
      document.getElementById("TenPhim").value = data.tenPhim;
      document.getElementById("TheLoai").value = data.theLoai;
      document.getElementById("ThoiLuong").value = data.thoiLuong;
      document.getElementById("KhoiChieu").value = data.khoiChieu;
      document.getElementById("MoTa").value = data.moTa;
      // Hiển thị ảnh hiện tại
      const img = document.getElementById("Anh");
      if (data.anh) {
        img.src = `https://localhost:7074/images/${data.Anh}`;
        img.style.display = 'block';
      } else {
        img.style.display = 'none';
      }
    } else {
      alert("Không tìm thấy dữ liệu.");
    }
  } catch (error) {
    console.error("Lỗi:", error);
    alert("Có lỗi xảy ra khi tải dữ liệu.");
  }
}

// Xử lý khi trang được tải
document.addEventListener("DOMContentLoaded", () => {
  // Nạp dữ liệu  phim vào bảng
  const urlParams = new URLSearchParams(window.location.search);
  const id = urlParams.get("id");

  if (id) {
    loadData(id);
  }

  // Xử lý form sửa thông tin
  const form = document.getElementById("editForm1");

  form.addEventListener("submit", async (event) => {
    event.preventDefault();

    const id = new URLSearchParams(window.location.search).get("id");
    const formData = new FormData(form);
    formData.append('maRap', id);

    try {
      // Gửi yêu cầu PUT đến API
      const response = await fetch(`${API}/${id}`, {
        method: "PUT",
        body: formData,
      });

      if (response.ok) {
        alert("Cập nhật thành công!");
        window.location.href = "list.html";
      } else {
        alert("Có lỗi xảy ra khi cập nhật.");
        console.error("Update failed:", response.status);
      }
    } catch (error) {
      console.error("Lỗi:", error);
      alert("Có lỗi xảy ra khi cập nhật.");
    }
  });
});