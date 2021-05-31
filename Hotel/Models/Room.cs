using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.Helpers;

namespace Hotel.Models
{
  public class Room
  {
    public double? danhGia { get; set; }
    public string hinhAnh { get; set; }

    public string maLoai { get; set; }
    public string tenPhong { get; set; }
    public int maPhong { get; set; }

    public decimal? giamGia { get; set; }
    public decimal? giaPhong { get; set; }

    public string tenLP { get; set; }

    public DateTime ngayDat { get; set; }
    public DateTime ngayTra { get; set; }

    public bool inStock { get; set; }
    public string tinhTrang { get; set; }

    public List<string> maDichVus { get; set; }

    public Room() { }

    public Room(string tenPhong)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        var tList = from ph in db.Phongs
                    join lp in db.LoaiPhongs
                    on ph.maLoai equals lp.maLoai
                    select new
                    {
                      danhGia = ph.danhGia,
                      hinhAnh = ph.hinhAnh,
                      maLoai = ph.maLoai,
                      giamGia = ph.giamGia,
                      giaPhong = ph.giaPhong,
                      tenLP = lp.tenlp,
                      maPhong = ph.maPhong,
                      tenPhong = ph.tenPhong,
                      tinhTrang = ph.tinhTrang,
                    };

        var result = tList.FirstOrDefault(item => item.tenPhong == tenPhong);

        this.danhGia = result.danhGia;
        this.hinhAnh = result.hinhAnh;

        this.maLoai = result.maLoai;
        this.giamGia = result.giamGia;

        this.giaPhong = result.giaPhong;
        this.tenLP = result.tenLP;

        this.tenPhong = result.tenPhong;
        this.maPhong = result.maPhong;
        this.tinhTrang = result.tinhTrang;

        this.maDichVus = new List<string>();
      }
    }

    public decimal calculateServiceMoney()
    {
      List<DichVu> listDichVu = DBDichVu.getDichVus();
      decimal tienDichVu = 0;

      foreach (string maDichVu in maDichVus)
      {
        tienDichVu += Decimal.Parse(listDichVu.FirstOrDefault(item => item.maDichVu == int.Parse(maDichVu)).giaTien.ToString());
      }

      return tienDichVu;
    }

    public decimal calculateTotalMoney()
    {
      var totalDays = ngayTra.Subtract(ngayDat).Days;
      decimal sum = 0;

      if (giamGia == null)
      {
        sum += Decimal.Parse((totalDays * giaPhong + calculateServiceMoney()).ToString());
      }
      else
      {
        sum += Decimal.Parse((totalDays * (giaPhong - giamGia) + calculateServiceMoney()).ToString());
      }

      return sum;
    }
  }
}