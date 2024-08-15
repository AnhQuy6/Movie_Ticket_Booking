create database db_xemphim;
drop database db_xemphim;
use db_xemphim;
use master;

﻿CREATE TABLE RAP (
    MaRap  INT IDENTITY(1,1) PRIMARY KEY, 
    TenRap VARCHAR(30) NOT NULL, 
    DiaChi VARCHAR(30) NOT NULL,
    Anh    VARCHAR(255), 
    SDT    VARCHAR(255)
);

CREATE TABLE PHIM (
    MaPhim    INT IDENTITY(1,1) PRIMARY KEY, 
    TenPhim   VARCHAR(30) NOT NULL, 
    TheLoai   VARCHAR(255) NOT NULL, 
    ThoiLuong VARCHAR(30) NOT NULL, 
    KhoiChieu TIME, 
    Anh       VARCHAR(255), 
    MoTa      TEXT
);

CREATE TABLE PHONG (
    MaPhong    INT IDENTITY(1,1) PRIMARY KEY, 
    MaRap      INT NOT NULL, 
    TenPhong   VARCHAR(255) NOT NULL, 
    SoLuongGhe INT NOT NULL,
    FOREIGN KEY (MaRap) REFERENCES RAP(MaRap)
);

CREATE TABLE SUATCHIEU (
    MaSuatChieu    INT IDENTITY(1,1) PRIMARY KEY, 
    MaPhim         INT NOT NULL, 
    MaPhong        INT NOT NULL, 
    NgayChieu      DATETIME, 
    ThoiGianBatDau TIME,
    FOREIGN KEY (MaPhim) REFERENCES PHIM(MaPhim),
    FOREIGN KEY (MaPhong) REFERENCES PHONG(MaPhong)
);

CREATE TABLE TAIKHOAN (
    MaTK         INT IDENTITY(1,1) PRIMARY KEY, 
    TenTK        VARCHAR(100) NOT NULL, 
    MatKhau      VARCHAR(100) NOT NULL, 
    DiaChi       VARCHAR(100), 
    NgaySinh     DATE, 
    Email        VARCHAR(100), 
    SDT          VARCHAR(100), 
    TenNguoiDung VARCHAR(100) NOT NULL
);

CREATE TABLE VEPHIM (
    MaVe        INT IDENTITY(1,1) PRIMARY KEY, 
    MaSuatChieu INT NOT NULL, 
    MaTK        INT NOT NULL, 
    GiaVe       INT, 
    PTTT        VARCHAR(255), 
    ViTri       INT,
    FOREIGN KEY (MaTK) REFERENCES TAIKHOAN(MaTK),
    FOREIGN KEY (MaSuatChieu) REFERENCES SUATCHIEU(MaSuatChieu)
);
select * from VEPHIM

insert into TAIKHOAN values (1, 'admin', 'admin123', 'HN', '2003-02-23', 'admin@gmail.com', '0977572993', 'admin')

INSERT INTO RAP (TenRap, DiaChi, Anh, SDT)  
VALUES ('CGV Cinema', N'Aeon Hà Đông', '../../../../images/rapcgv.jpg', '0987654321');  

INSERT INTO RAP (TenRap, DiaChi, Anh, SDT)  
VALUES ('Lotte Cinema',N'Mê Linh Plaza', '../../../../images/raplotte.jpg', '0112233445');

INSERT INTO PHIM (TenPhim, TheLoai, ThoiLuong, KhoiChieu, Anh, MoTa)
VALUES 
('Conan', 'Hành động, Trinh thám', '90 phút', '14:00:00', '../../../../images/conan.jpg', 'Thám tử lừng danh Conan là một bộ phim trinh thám nổi tiếng của Nhật Bản.'),
('Minion', 'Hoạt hình, Hài hước', '85 phút', '18:00:00', '../../../../images/minion.jpg', 'Minion là bộ phim hài hước về những sinh vật màu vàng vui nhộn.');

Delete from RAP where MaRap =15

INSERT INTO PHIM (TenPhim, TheLoai, ThoiLuong, KhoiChieu, Anh, MoTa)
VALUES 
('Minion', 'Hoạt hình, Hài hước', '85 phút', '18:00:00', '../../../../minion.jpg', 'Minion là bộ phim hài hước về những sinh vật màu vàng vui nhộn.');

-- Thêm dữ liệu cho Rạp MaRap 17
INSERT INTO PHONG (MaRap, TenPhong, SoLuongGhe) VALUES (1, N'Phòng 1', 65);
INSERT INTO PHONG (MaRap, TenPhong, SoLuongGhe) VALUES (1, N'Phòng 2', 65);
INSERT INTO PHONG (MaRap, TenPhong, SoLuongGhe) VALUES (1, N'Phòng 3', 65);

-- Thêm dữ liệu cho Rạp MaRap 18
INSERT INTO PHONG (MaRap, TenPhong, SoLuongGhe) VALUES (2, N'Phòng 1', 65);
INSERT INTO PHONG (MaRap, TenPhong, SoLuongGhe) VALUES (2, N'Phòng 2', 65);
INSERT INTO PHONG (MaRap, TenPhong, SoLuongGhe) VALUES (2, N'Phòng 3', 65);
-- Thêm dữ liệu cho Phòng 1 của Rạp MaRap 17
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (1, 1, '2024-08-15', '10:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (1, 1, '2024-08-15', '13:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (1, 1, '2024-08-15', '16:00:00');

-- Thêm dữ liệu cho Phòng 2 của Rạp MaRap 17
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (2, 2, '2024-08-15', '10:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (2, 2, '2024-08-15', '13:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (2, 2, '2024-08-15', '16:00:00');

-- Thêm dữ liệu cho Phòng 3 của Rạp MaRap 17
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (3, 3, '2024-08-15', '10:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (3, 3, '2024-08-15', '13:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (3, 3, '2024-08-15', '16:00:00');

-- Thêm dữ liệu cho Phòng 1 của Rạp MaRap 18
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (4, 4, '2024-08-15', '10:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (4, 4, '2024-08-15', '13:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (4, 4, '2024-08-15', '16:00:00');

-- Thêm dữ liệu cho Phòng 2 của Rạp MaRap 18
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (5, 5, '2024-08-15', '10:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (5, 5, '2024-08-15', '13:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (5, 5, '2024-08-15', '16:00:00');

-- Thêm dữ liệu cho Phòng 3 của Rạp MaRap 18
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (6, 6, '2024-08-15', '10:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (6, 6, '2024-08-15', '13:00:00');
INSERT INTO SUATCHIEU (MaPhim, MaPhong, NgayChieu, ThoiGianBatDau) VALUES (6, 6, '2024-08-15', '16:00:00');


delete from TAIKHOAN

delete from PHIM