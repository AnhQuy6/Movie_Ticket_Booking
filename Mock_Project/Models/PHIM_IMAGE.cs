﻿namespace Mock_Project.Models
{
    public class PHIM_IMAGE
    {
        public int MaPhim { get; set; }

        public string TenPhim { get; set; }

        public string TheLoai { get; set; }

        public string ThoiLuong { get; set; }

        public TimeOnly KhoiChieu { get; set; }

        //public string Anh { get; set; }

        public string MoTa { get; set; }
        public IFormFile Anh { get; set; }
        
    }
}
