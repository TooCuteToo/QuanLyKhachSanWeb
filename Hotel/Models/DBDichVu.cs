using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.Models;

namespace Hotel.Helpers
{
  public class DBDichVu
  {
    public static List<DichVu> getDichVus()
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        return db.DichVus.ToList();
      }
    }

    public static List<ChiTietDichVu> getDichVusByMa(int maHD)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        return db.ChiTietDichVus.Where(item => item.maHD == maHD).ToList();
      }
    }

    public static void createServicesDetail(string tenPhong, List<string> services)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        HoaDon hd = db.HoaDons.FirstOrDefault(item => item.tenPhong == tenPhong);

        if (services == null)
        {
          ChiTietDichVu ct = new ChiTietDichVu()
          {
            maHD = hd.maHD,
            maDichVu = 6,
          };

          db.ChiTietDichVus.InsertOnSubmit(ct);
          db.SubmitChanges();

          hd.tienThanhToan += getTotalServicesMoney(hd);
          db.SubmitChanges();

          return;
        }

        List<ChiTietDichVu> listServices = new List<ChiTietDichVu>();

        foreach (string service in services)
        {
          ChiTietDichVu ct = new ChiTietDichVu()
          {
            maHD = hd.maHD,
            maDichVu = int.Parse(service),
          };

          listServices.Add(ct);

        }

        db.ChiTietDichVus.InsertAllOnSubmit(listServices);
        db.SubmitChanges();

        db.HoaDons.FirstOrDefault(item => item.maHD == hd.maHD).tienThanhToan += getTotalServicesMoney(hd);
        db.SubmitChanges();
      }
    }

    public static void removeServicesDetail(HoaDon hd)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        List<ChiTietDichVu> services = db.ChiTietDichVus.Where(item => item.maHD == hd.maHD).ToList();

        foreach (ChiTietDichVu service in services)
        {
          db.ChiTietDichVus.DeleteOnSubmit(service);
          db.SubmitChanges();
        }
      }
    }

    public static decimal getTotalServicesMoney(HoaDon hd)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        List<ChiTietDichVu> ctDichVus = db.ChiTietDichVus.Where(item => item.maHD == hd.maHD).ToList();

        decimal totalMoney = 0;

        foreach (ChiTietDichVu ct in ctDichVus)
        {
          totalMoney += Decimal.Parse(db.DichVus.FirstOrDefault(item => item.maDichVu == ct.maDichVu).giaTien.ToString());
        }

        return totalMoney;
      }
    }
  }
}