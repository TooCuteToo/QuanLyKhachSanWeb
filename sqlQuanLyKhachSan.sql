use master
create database QuanLyKhachSan
go

use QuanLyKhachSan
go

create table ChucVu
(
	MaCV varchar(10),
	TenCV nvarchar(100),
	CONSTRAINT PK_chucVu PRIMARY KEY(MaCV)
)

create table NhanVien
(
	MaNV int identity(1, 1),
	TenNV nvarchar(100),
	Email varchar(30) unique,
	Password varchar(30),
	MaCV varchar(10),
	DiaChi_NV nvarchar(200),
	SDT_NV varchar(10),
	GioiTinh nvarchar(10),
	NgaySinh date,
	CONSTRAINT PK_NhanVien PRIMARY KEY(MaNV),
	CONSTRAINT FK_NhanVien_ChucVu FOREIGN KEY(MaCV) REFERENCES ChucVu(MaCV)
)

create table KhachHang
(
maKH int identity(1, 1) primary key,
email varchar(30) unique,
tenKH nvarchar(100) not null,
gioiTinh nvarchar(100),
ngaySinh date,
diaChi nvarchar(100),
sdt nvarchar(10),
pass nvarchar(10)
)
go

create table LoaiPhong
(
maLoai varchar(10) primary key,
tenlp nvarchar(100),
)
go

create table Phong
(
maPhong int identity(1, 1) primary key,
tenPhong varchar(30) unique not null,
tinhTrang nvarchar(50) not null,
maLoai varchar(10) not null,
hinhAnh varchar(30) not null,
giaPhong money,
giamGia money,
danhGia float,
constraint phong_malp foreign key (maLoai) references LoaiPhong(maLoai)
)
go

create table DichVu (
	maDichVu int identity(1, 1) primary key,
	tenDichVu varchar(50),
	giaTien money
)

create table HoaDon
(
maHD int identity(1, 1) primary key,
maKH int,
maNV int,
email varchar(30),
tenPhong varchar(30),
maPhong int,
ngayDat date,
ngayTra date,
tinhTrang bit,
tienThanhToan money,
constraint Hoadon_maPhong foreign key (maPhong) references Phong(maPhong),
constraint HoaDon_MAKH foreign key (maKH) references KhachHang(maKH),
constraint HoaDon_MANV foreign key (maNV) references NhanVien(maNV)
)
go

create table ChiTietDichVu (
	maHD int,
	maDichVu int,

	constraint PK_CTDV primary key (maHD, maDichVu),
	constraint FK_maHD foreign key (maHD) references HoaDon(maHD),
	constraint FK_maDichVu foreign key (maDichVu) references DichVu(maDichVu)
)

insert into ChucVu(MaCV, TenCV) values
('admin',N'Administrator'),
('employee',N'Employee')

set dateformat DMY
insert into NhanVien(TenNV, Email, Password, MaCV, DiaChi_NV, SDT_NV, GioiTinh, NgaySinh) values
(N'Nguyen Phuong Thao', 'thao@gmail.com', '123456', 'admin', N'123 Tan Ky Tan Quy', '0923456765', N'Male', '11/08/2001'),
(N'Huynh Minh Anh', 'anh@gmail.com', '123456', 'employee',N'88 Le Trong Tan','098354555',N'Female','08/06/2000')

set dateformat DMY
insert into KhachHang (email, tenKH, gioiTinh, ngaySinh, diaChi, sdt, pass)
values
('my@gmail.com',N'Vu Thi Hai My',N'Female', '15/01/2003', 'Truong Chinh', '0983004421', N'123'),
('luan@gmail.com',N'Nguyen Le Hoang Luan',N'Male', '29/05/2001', 'Truong Chinh', '0983004421', N'123'),
('toan@gmail.com',N'Tran Dinh Toan',N'Male', '10/02/2000', 'Truong Chinh', '0983004421', N'123'),
('anh@gmail.com',N'Le Thi Anh',N'Female', '12/06/2002', 'Truong Chinh', '0983004421', N'123'),
('lam@gmail.com',N'Ngo Gia Lam',N'Male', '20/02/1999', 'Truong Chinh', '0983004421', N'123')


insert into LoaiPhong  values
('lp01',N'1 PERSON'),
('lp02',N'2 PEOPLE'),
('lp03',N'4 PEOPLE')

INSERT INTO Phong values
('Summer Room',N'empty','lp01', 'hinh_0.jpg', 129.99, 50.99, 4.5),
('Cool Room',N'empty','lp01', 'hinh_1.jpg', 130.99, 30.99, 3.2),
('Fair Room',N'empty','lp02', 'hinh_2.jpg', 70.99, null, 2.4),
('Nice Room',N'empty','lp02', 'hinh_3.jpg', 80.99, 23.99, 3.5),
('lp Room',N'empty','lp03', 'hinh_4.jpg', 138.99, 35.99, 4.8),
('LUX Room',N'empty','lp03', 'hinh_5.jpg', 250.99, 70.99, 5),
('Night Room',N'empty','lp03', 'hinh_6.jpg', 170.99, null, 2.2),
('Morning Room',N'empty','lp01', 'hinh_7.jpg', 25.99, 5.99, 3.1),
('Jan Room',N'empty','lp01', 'hinh_8.jpg', 30.99, 4.99, 3.2),
('Nock Room',N'empty','lp02', 'hinh_9.jpg', 32.5, 12.99, 4.1),
('No Room',N'empty','lp02', 'hinh_10.jpg', 70.99, null, 2.3),
('Black Room',N'empty','lp03', 'hinh_11.jpg', 80.99, 28.99, 3.8),
('Null Room',N'empty','lp03', 'hinh_12.jpg', 35.99, null, 2.3),
('Dart Room',N'empty','lp01', 'hinh_13.jpg', 67.99, null, 3.2),
('Boot Room',N'empty','lp02', 'hinh_14.jpg', 80.99, 29.99, 4.1),
('Nine Room',N'empty','lp03', 'hinh_15.jpg', 36.99, 29.99, 2.9),
('Foul Room',N'empty','lp03', 'hinh_16.jpg', 72.99, null, 1.1),
('Sin Room',N'empty','lp02', 'hinh_17.jpg', 80.99, 50.99, 3.2),
('Pok Room',N'empty','lp01', 'hinh_18.jpg', 30.99, 8.99, 4.8)
go

insert into DichVu(tenDichVu, giaTien)
values('Make-up room', 20.99),
('Tranportation', 10.99),
('Money exchance', 2.99),
('Bell', 1.99),
('Catering', 30.99),
('None', 0)

drop table ChucVu
drop table NhanVien

drop table LoaiPhong
drop table Phong
drop table HoaDon
drop table KhachHang

select * from LoaiPhong
select * from Phong
select * from HoaDon
select * from NhanVien
select * from KhachHang
select * from DichVu
select * from ChiTietDichVu

update Phong
set TinhTrang = 'occupied'
where tenPhong = 'Nock Room'

select * from KhachHang
where maKH = 6

delete HoaDon
where maHD = 4
