using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;
using Hotel.Helpers;

namespace Hotel.Controllers
{
  public class CustomerController : Controller
  {
    //
    // GET: /Customer/

    [HttpGet]
    public ActionResult Login(int id = 0)
    {
      ViewBag.id = id;
      return View();
    }

    [HttpGet]
    public ActionResult Logout()
    {
      Session["kh"] = null;
      Session["cart"] = null;

      return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public JsonResult Login(KhachHang loginCustomer, int id = 0)
    {
      if (id == 0)
      {
        KhachHang kh = DBCustomer.getCustomer(loginCustomer);

        if (kh != null)
        {
          Session["kh"] = kh;
          return Json(new { message = "Success" });
        }

        return Json(new { message = "Fail" });
      }

      loginCustomer.gioiTinh = "Female";
      KhachHang newCustomer = DBCustomer.createCustomer(loginCustomer);

      if (newCustomer.tenKH != null)
      {
        return Json(new { newCustomer = loginCustomer });

      }

      return Json(new { message = "Email is already existed!!!" });
    }
    

    public ActionResult Profile()
    {
      return View();
    }

    [HttpPost]
    public JsonResult UpdateCustomer(KhachHang kh)
    {
      KhachHang result = DBCustomer.UpdateCustomer(kh);
      KhachHang loginNv = (KhachHang)Session["kh"];

      Session["kh"] = kh;
      return Json(new { url = "/Customer/Profile" });
    }


    //[HttpPost]
    //public ActionResult ThemPhongYeuThich(string tenPhong)
    //{
    //    using (DataClasses1DataContext db = new DataClasses1DataContext())
    //    {
    //        KhachHang kh = Session["kh"] as KhachHang;

    //        if (kh == null)
    //        {
    //            return RedirectToAction("Login", "Customer", new { id = 0 });
    //        }

    //        Room phong = new Room(tenPhong);

    //        db.PhongYeuThiches.InsertOnSubmit(new PhongYeuThich()
    //        {
    //            tenPhong = phong.tenPhong,
    //            email = kh.email,
    //        });

    //        db.SubmitChanges();

    //        return RedirectToAction("Index", "Home");
    //    }
    //}
  }
}
