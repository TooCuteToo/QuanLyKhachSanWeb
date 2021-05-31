using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.Models;

namespace Hotel.Helpers
{
  public class DBHoaDon
  {
    public static List<HoaDon> getHoaDons(KhachHang kh)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        List<HoaDon> dsHoaDon = db.HoaDons.Where(item => item.email == kh.email).ToList();

        return dsHoaDon;
      }
    }

    public static HoaDon getHoaDon(string tenPhong)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        return db.HoaDons.FirstOrDefault(item => item.tenPhong == tenPhong);
      }
    }

    public static void createHoaDon(List<Room> listPhong, KhachHang kh)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        foreach (Room phong in listPhong)
        {
          int totalDays = phong.ngayTra.Subtract(phong.ngayDat).Days;
          Random random = new Random();
          int numberOfNhanVien = db.NhanViens.Count();

          HoaDon hd = new HoaDon()
          {
            maNV = random.Next(numberOfNhanVien) + 1,
            maKH = kh.maKH,
            maPhong = phong.maPhong,
            email = kh.email,
            ngayDat = phong.ngayDat,
            ngayTra = phong.ngayTra,
            tenPhong = phong.tenPhong,
            tinhTrang = false,
            tienThanhToan = phong.giamGia != null ?(phong.giaPhong - phong.giamGia) * totalDays : phong.giaPhong * totalDays,
          };

          db.Phongs.FirstOrDefault(item => item.tenPhong == phong.tenPhong).tinhTrang = "occupied";

          db.HoaDons.InsertOnSubmit(hd);
          db.SubmitChanges();

        }
      }
    }

    public static void removeHoaDon(string tenPhong)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        HoaDon hd = db.HoaDons.FirstOrDefault(item => item.tenPhong == tenPhong);
        db.Phongs.FirstOrDefault(item => item.tenPhong == tenPhong).tinhTrang = "empty";

        db.HoaDons.DeleteOnSubmit(hd);
        db.SubmitChanges();
      }
    }
  }
}