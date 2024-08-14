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
        <td><img src="${phim.anh}" alt="Ảnh rạp phim" class="img-thumbnail" style="max-width: 100px;"></td>
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
      document.getElementById("Anh").value = data.anh;
      document.getElementById("MoTa").value = data.moTa;
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
  // Nạp dữ liệu rạp phim vào bảng
  loadRapData();
  // Xử lý form sửa thông tin
  const form = document.getElementById("editForm1");
  const urlParams = new URLSearchParams(window.location.search);
  const id = urlParams.get("id"); 

  if (id) {
    form.setAttribute("data-id", id);
    loadData(id); 
  }
  form.addEventListener("submit", async (event) => {
    event.preventDefault(); 

    const id = form.getAttribute("data-id");
    console.log(id);
    // Thu thập dữ liệu từ form

    const tenPhim = document.getElementById("TenPhim").value 
    const theLoai = document.getElementById("TheLoai").value;
    const thoiLuong = document.getElementById("ThoiLuong").value;
    const khoiChieu = document.getElementById("KhoiChieu").value;
    const khoiChieuFormatted = khoiChieu + ":00"; 
    const anh = document.getElementById("Anh").value;
    const moTa = document.getElementById("MoTa").value;

    const updatedData = {
      maPhim: id,
      tenPhim: tenPhim,
      theLoai: theLoai,
      thoiLuong: thoiLuong,
      khoiChieu: khoiChieu,
      anh: anh,
      moTa: moTa,
    };
    console.log(updatedData);
    try {
      // Gửi yêu cầu PUT đến API
      const response = await fetch(`${API}/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(updatedData),
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