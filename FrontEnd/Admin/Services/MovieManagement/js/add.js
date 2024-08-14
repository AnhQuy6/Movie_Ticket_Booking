const API = "https://localhost:7074/api/Phim";
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
