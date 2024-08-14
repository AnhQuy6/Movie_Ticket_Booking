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

//Sửa rạp phim
//Load old data
async function loadData(id) {
  try {
    const response = await fetch(`${API}/${id}`);
    if (response.ok) {
      const data = await response.json();
      document.getElementById("tenRap").value = data.tenRap;
      document.getElementById("diaChi").value = data.diaChi;
      document.getElementById("anh").value = data.anh;
      document.getElementById("soDienThoai").value = data.sdt;
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
  const form = document.getElementById("editForm");
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
    const tenRap = document.getElementById("tenRap").value;
    const diaChi = document.getElementById("diaChi").value;
    const anh = document.getElementById("anh").value;
    const soDienThoai = document.getElementById("soDienThoai").value;

    const updatedData = {
      maRap: id,
      tenRap: tenRap,
      diaChi: diaChi,
      anh: anh,
      soDienThoai: soDienThoai,
    };

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
