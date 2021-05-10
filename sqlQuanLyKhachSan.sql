use master
create database QuanLyKhachSan
go

use QuanLyKhachSan
go

create table ChucVu
(
	MaCV varchar(11) not null,
	TenCV nvarchar(100),
	Luong money,
	CONSTRAINT PK_chucVu PRIMARY KEY(MaCV)
)

create table NhanVien
(
	MaNV varchar(11) not null,
	TenNV nvarchar(100),
	MaCV varchar(11),
	DiaChi_NV nvarchar(200),
	SDT_NV varchar(10),
	GioiTinh nvarchar(5),
	NgayVaoLam datetime,
	CONSTRAINT PK_NhanVien PRIMARY KEY(MaNV),
	CONSTRAINT FK_NhanVien_ChucVu FOREIGN KEY(MaCV) REFERENCES ChucVu(MaCV)
)

create table KhachHang
(
email varchar(30) primary key,
tenKH nvarchar(100) not null,
gioiTinh nvarchar(100),
pass nvarchar(10),
)
go

create table LoaiPhong
(
maLoai varchar(10) primary key,
tenLP nvarchar(100),
)
go

create table Phong
(
tenPhong varchar(30) primary key,
tinhTrang nvarchar(50) not null,
maLoai varchar(10) not null,
hinhAnh varchar(30) not null,
giaPhong money,
giamGia money,
danhGia float,
constraint phong_maLP foreign key (maLoai) references LoaiPhong(maLoai)
)
go

create table HoaDon
(
maHD varchar(10) primary key,
email varchar(30) not null,
tenPhong varchar(30) not null,
ngayDat date,
ngayTra date,
tienThanhToan money,
constraint Hoadon_maPhong foreign key (tenPhong) references Phong(tenPhong),
constraint Hoadon_CMND foreign key (email) references KhachHang(email)
)
go

insert into ChucVu(MaCV, TenCV, Luong) values
('admin',N'Administrator',20000000),
('user',N'user',10000000)

set dateformat DMY
insert into NhanVien(MaNV,TenNV,MaCV,DiaChi_NV,SDT_NV,GioiTinh,NgayVaoLam) values
('NV001', N'Nguyễn Phương Thảo', 'admin', N'123,Tân Kì Tân Quý, Quận Tân Phú,tp Hồ Chí Minh', '0923456765', N'Nữ', '11/08/2019'),
('NV002',N'Huỳnh Minh Anh','user',N'88, Lê Trọng Tấn, Quận Tân Phú, tp Hồ Chí Minh','098354555',N'Nữ','08/06/2019'),
('NV003',N'Trần Hữu Minh','user',N'100,Tỉnh lộ 10, Quận Bình Tân,tp Hồ Chí Minh','0983322344',N'Nam','07/01/2019'),
('NV004',N'Nguyễn Minh Nhật','user',N'100,Tên lửa, Quận Bình Tân,tp Hồ Chí Minh','0983433434',N'Nam','20/03/2019'),
('NV005',N'Đào Ánh Mai','user',N'14,Đường số 7, Quận Bình Tân,tp Hồ Chí Minh','0983343434',N'Nữ','20/12/2018'),		
('NV006',N'Nguyễn Ánh Nguyệt','user',N'14,Đường ab, Quận Long Biên,Hà Nội','0983343434',N'Nữ','15/10/2018'),
('NV007',N'Hoàng Mai Linh','user',N'14,Đường ab, Quận Long Biên,Hà Nội','0983343434',N'Nữ','15/10/2018')


insert into KhachHang values
('my@gmail.com',N'Vũ Thị Hải My',N'Nữ', N'123'),
('luan@gmail.com',N'Nguyễn Hoàng Luân',N'Nam', N'123'),
('toan@gmail.com',N'Trần Đình Toàn',N'Nam', N'123'),
('anh@gmail.com',N'Lê Thị Ngọc Ánh',N'Nữ', N'123'),
('lam@gmail.com',N'Ngô Gia Lâm',N'Nam', N'123')


insert into LoaiPhong  values
('LP01',N'PHÒNG 1 NGƯỜI'),
('LP02',N'PHÒNG 2 NGƯỜI'),
('LP03',N'PHÒNG 4 NGƯỜI')

INSERT INTO Phong values
('Summer Room',N'Trống','LP01', 'hinh_0.jpg', 129.99, 50.99, 4.5),
('Cool Room',N'Trống','LP01', 'hinh_1.jpg', 130.99, 30.99, 3.2),
('Fair Room',N'Trống','LP02', 'hinh_2.jpg', 70.99, null, 2.4),
('Nice Room',N'Trống','LP02', 'hinh_3.jpg', 80.99, 23.99, 3.5),
('LP Room',N'Trống','LP03', 'hinh_4.jpg', 138.99, 35.99, 4.8),
('LUX Room',N'Trống','LP03', 'hinh_5.jpg', 250.99, 70.99, 5),
('Night Room',N'Trống','LP03', 'hinh_6.jpg', 170.99, null, 2.2),
('Morning Room',N'Trống','LP01', 'hinh_7.jpg', 25.99, 5.99, 3.1),
('Jan Room',N'Trống','LP01', 'hinh_8.jpg', 30.99, 4.99, 3.2),
('Nock Room',N'Trống','LP02', 'hinh_9.jpg', 32.5, 12.99, 4.1),
('No Room',N'Trống','LP02', 'hinh_10.jpg', 70.99, null, 2.3),
('Black Room',N'Trống','LP03', 'hinh_11.jpg', 80.99, 28.99, 3.8),
('Null Room',N'Trống','LP03', 'hinh_12.jpg', 35.99, null, 2.3),
('Dart Room',N'Trống','LP01', 'hinh_13.jpg', 67.99, null, 3.2),
('Boot Room',N'Trống','LP02', 'hinh_14.jpg', 80.99, 29.99, 4.1),
('Nine Room',N'Trống','LP03', 'hinh_15.jpg', 36.99, 29.99, 2.9),
('Foul Room',N'Trống','LP03', 'hinh_16.jpg', 72.99, null, 1.1),
('Sin Room',N'Trống','LP02', 'hinh_17.jpg', 80.99, 50.99, 3.2),
('Pok Room',N'Trống','LP01', 'hinh_18.jpg', 30.99, 8.99, 4.8)
go

drop table LoaiPhong
drop table Phong
drop table HoaDon

select * from LoaiPhong
select * from Phong
select * from HoaDon
