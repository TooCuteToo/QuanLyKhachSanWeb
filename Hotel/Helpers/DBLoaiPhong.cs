using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.Models;

namespace Hotel.Helpers
{
  public class DBLoaiPhong
  {
    public static string getTenLoaiPhong(string maLoai)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        return db.LoaiPhongs.FirstOrDefault(item => item.maLoai == maLoai).tenlp;
      }
    }

    public static List<LoaiPhong> getListLoaiPhong()
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        return db.LoaiPhongs.ToList();
      }
    }
  }
}