using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.Models;

namespace Hotel.Helpers
{
  public class DBCustomer
  {
    public static KhachHang getCustomer(KhachHang loginCustomer)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        db.DeferredLoadingEnabled = false;
        KhachHang kh = db.KhachHangs.FirstOrDefault(item => item.email == loginCustomer.email && item.pass == loginCustomer.pass);

        return kh;
      }
    }

    public static KhachHang createCustomer(KhachHang newCustomer)
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        try
        {
          db.DeferredLoadingEnabled = false;

          newCustomer.gioiTinh = "Female";

          db.KhachHangs.InsertOnSubmit(newCustomer);
          db.SubmitChanges();

          return newCustomer;

        }
        catch
        {
          return new KhachHang() { tenKH = null };
        }
      }
    }
  }
}