using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.Models;

namespace Hotel.Helpers
{
  public class DBPhong
  {
    public static List<Room> getSuggestRoom(Room phong)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        var tList = from ph in db.Phongs
                    join lp in db.LoaiPhongs
                    on ph.maLoai equals lp.maLoai
                    select new Room
                    {
                      danhGia = ph.danhGia,
                      tenPhong = ph.tenPhong,
                      hinhAnh = ph.hinhAnh,
                      giamGia = ph.giamGia,
                      giaPhong = ph.giaPhong,
                      maLoai = lp.maLoai,
                      tenLP = lp.tenlp,
                      maPhong = ph.maPhong,
                      tinhTrang = ph.tinhTrang,
                    };

        return tList.Where(item => item.maLoai != phong.maLoai).Skip(tList.Count() - tList.Count() / 2).ToList();
      }
    }

    public static List<Room> getListRoom()
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        var tList = from ph in db.Phongs
                    join lp in db.LoaiPhongs
                    on ph.maLoai equals lp.maLoai
                    select new Room()
                    {
                      danhGia = ph.danhGia,
                      tenPhong = ph.tenPhong,
                      giamGia = ph.giamGia,
                      giaPhong = ph.giaPhong,
                      hinhAnh = ph.hinhAnh,
                      tenLP = lp.tenlp,
                      maLoai = lp.maLoai,
                    };

        return tList.ToList();
      }
    }

    public static Room getRoom(string id)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        var tList = from ph in db.Phongs
                    join lp in db.LoaiPhongs
                    on ph.maLoai equals lp.maLoai
                    select new Room
                    {
                      danhGia = ph.danhGia,
                      tenPhong = ph.tenPhong,
                      hinhAnh = ph.hinhAnh,
                      giamGia = ph.giamGia,
                      giaPhong = ph.giaPhong,
                      maLoai = lp.maLoai,
                      tenLP = lp.tenlp,
                      maPhong = ph.maPhong,
                      tinhTrang = ph.tinhTrang,
                    };

        Room phong = tList.ToList().FirstOrDefault(item => item.tenPhong.Contains(id));

        return phong;
      }
    }
  }
}