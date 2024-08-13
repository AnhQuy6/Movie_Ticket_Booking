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
// Thêm
document.addEventListener("DOMContentLoaded", () => {
  const form = document.querySelector("form");

  form.addEventListener("submit", async (event) => {
    event.preventDefault();

    // Thu thập dữ liệu từ form
      const tenPhim = document.getElementById("TenPhim").value;
      const theLoai = document.getElementById("TheLoai").value;
      const thoiLuong = document.getElementById("ThoiLuong").value;
      const khoiChieu = document.getElementById("KhoiChieu").value;
      const khoiChieuFormatted = khoiChieu + ":00"; 
      const anh = document.getElementById("Anh").value;
      const moTa = document.getElementById("MoTa").value;

      const newRap = {
        tenPhim: tenPhim,
        theLoai: theLoai,
        thoiLuong: thoiLuong,
        khoiChieu: khoiChieuFormatted,
        anh: anh,
        moTa: moTa,
      };
      console.log(newRap);
    try {
      // Gửi yêu cầu POST đến API
      const response = await fetch(API, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newRap),
      });

      if (response.ok) {
        alert("Thêm phim thành công!");
        window.location.href = "list.html";
      } else {
        alert("Có lỗi xảy ra khi thêm rạp phim.");
        console.error("Add failed:", response.status);
      }
    } catch (error) {
      console.error("Lỗi:", error);
      alert("Có lỗi xảy ra khi thêm phim.");
    }
  });
});
