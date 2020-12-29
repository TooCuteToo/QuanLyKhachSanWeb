use master
create database QuanLyKhachSan
go

use QuanLyKhachSan
go


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
giaPhong money,
)
go

create table Phong
(
tenPhong varchar(30) primary key,
tinhTrang nvarchar(50) not null,
maLoai varchar(10) not null,
hinhAnh varchar(30) not null,
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



insert into KhachHang values
('my@gmail.com',N'Vũ Thị Hải My',N'Nữ', N'123'),
('luan@gmail.com',N'Nguyễn Hoàng Luân',N'Nam', N'123'),
('toan@gmail.com',N'Trần Đình Toàn',N'Nam', N'123'),
('anh@gmail.com',N'Lê Thị Ngọc Ánh',N'Nữ', N'123'),
('lam@gmail.com',N'Ngô Gia Lâm',N'Nam', N'123')


insert into LoaiPhong  values
('LP01',N'PHÒNG 1 NGƯỜI',99.99),
('LP02',N'PHÒNG 2 NGƯỜI',199.99),
('LP03',N'PHÒNG 4 NGƯỜI',299.99)

INSERT INTO Phong values
('Summer Room',N'Trống','LP01', 'hinh_0.jpg', 50.99, 4.5),
('Cool Room',N'Trống','LP01', 'hinh_1.jpg', 30.99, 3.2),
('Fair Room',N'Trống','LP02', 'hinh_2.jpg', null, 2.4),
('Nice Room',N'Trống','LP02', 'hinh_3.jpg', 123.99, 3.5),
('LP Room',N'Trống','LP03', 'hinh_4.jpg', 235.99, 4.8),
('LUX Room',N'Trống','LP03', 'hinh_5.jpg', 270.99, 5),
('Night Room',N'Trống','LP03', 'hinh_6.jpg', null, 2.2),
('Morning Room',N'Trống','LP01', 'hinh_7.jpg', 55.99, 3.1),
('Jan Room',N'Trống','LP01', 'hinh_8.jpg', 64.99, 3.2),
('Nock Room',N'Trống','LP02', 'hinh_9.jpg', 132.99, 4.1),
('No Room',N'Trống','LP02', 'hinh_10.jpg', null, 2.3),
('Black Room',N'Trống','LP03', 'hinh_11.jpg', 238.99, 3.8),
('Null Room',N'Trống','LP03', 'hinh_12.jpg', null, 2.3),
('Dart Room',N'Trống','LP01', 'hinh_13.jpg', null, 3.2),
('Boot Room',N'Trống','LP02', 'hinh_14.jpg', 129.99, 4.1),
('Nine Room',N'Trống','LP03', 'hinh_15.jpg', 229.99, 2.9),
('Foul Room',N'Trống','LP03', 'hinh_16.jpg', null, 1.1),
('Sin Room',N'Trống','LP02', 'hinh_17.jpg', 150.99, 3.2),
('Pok Room',N'Trống','LP01', 'hinh_18.jpg', 38.99, 4.8)
go

INSERT INTO HoaDon values
('HD01','anh@gmail.com', 'Nock Room','10/10/2020','13/10/2020', null)

INSERT INTO HoaDon values
('HD02','anh@gmail.com', 'Nock Room','10/10/2020','14/10/2020', null)

INSERT INTO HoaDon values
('HD03','anh@gmail.com','Nock Room','10/10/2020','15/10/2020', null)

INSERT INTO HoaDon values
('HD04','anh@gmail.com','Nock Room','10/10/2020','20/10/2020', null)

INSERT INTO HoaDon values
('HD05','anh@gmail.com','Nock Room','10/10/2020','21/10/2020', null)

INSERT INTO HoaDon values
('HD06','anh@gmail.com','Nock Room','10/10/2020','22/10/2020', null)

INSERT INTO HoaDon values
('HD07','anh@gmail.com','Nock Room','10/10/2020','23/10/2020', null)

INSERT INTO HoaDon values
('HD08','anh@gmail.com','Nock Room','10/10/2020','7/11/2020', null)

INSERT INTO HoaDon values
('HD09','anh@gmail.com','Nock Room','10/10/2020','15/11/2020', null)

INSERT INTO HoaDon values
('HD10','anh@gmail.com','Nock Room','10/10/2020','20/11/2020', null)
Go
