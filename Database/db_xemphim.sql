CREATE TABLE RAP (
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
    Anh       VARCHAR(30), 
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
