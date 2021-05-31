using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;
using Hotel.Helpers;

namespace Hotel.Controllers
{
  public class HomeController : Controller
  {
    //
    // GET: /Home/

    public ActionResult Index()
    {
      using (DataClasses1DataContext db = new DataClasses1DataContext())
      {
        return View("Index");
      }
    }

    public ActionResult KhoiTaoTrangThai()
    {
      KhachHang kh = Session["kh"] as KhachHang;

      return PartialView(kh);
    }

    public ActionResult KhoiTaoDanhSachPhong(string maLoai = null, string tenPhong = "")
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
                      maPhong = ph.maPhong,
                      maLoai = lp.maLoai,
                      tinhTrang = ph.tinhTrang,
                    };

        List<Room> listPhong = tList.OrderBy(item => new Random().Next()).ToList();

        Cart cart = (Cart)Session["cart"];

        if (cart != null)
        {
          foreach (Room phong in listPhong)
          {
            Room kq = cart.items.FirstOrDefault(item => item.tenPhong.Contains(phong.tenPhong));

            if (kq != null)
            {
              phong.inStock = true;
            }
          }
        }

        if (maLoai != null)
        {
          if (maLoai.Contains("lp"))
          {
            listPhong = listPhong.Where(item => item.maLoai == maLoai && item.tenPhong.ToLower().Contains(tenPhong)).ToList();

            return PartialView(listPhong);
          }

          return PartialView(listPhong);
        }

        listPhong = listPhong.Where(item => item.danhGia >= 4).ToList();

        return PartialView(listPhong);
      }
    }

    [HttpGet]
    public ActionResult Room(string maLoai = null, string tenPhong = "")
    {
      if (maLoai != null)
      {
        if (maLoai == "0")
        {
          ViewBag.tenLP = "Tất cả phòng";
          return View("Room", (object)maLoai);
        }

        ViewBag.tenLP = DBLoaiPhong.getTenLoaiPhong(maLoai);
        ViewBag.tenPhong = tenPhong.ToLower();

        return View("Room", (object)maLoai);
      }

      return View("Room", (object)maLoai);
    }

    [HttpPost]
    public ActionResult Room(FormCollection fc)
    {
      string tenPhong = fc["tenPhong"];
      string loaiPhong = fc["loaiPhong"];

      return RedirectToAction("Room", "Home", new { maLoai = loaiPhong, tenPhong });
    }

    public ActionResult KhoiTaoSubMenu()
    {
      List<LoaiPhong> listLP = DBLoaiPhong.getListLoaiPhong();

      return PartialView(listLP);
    }

    public ActionResult ContactUs()
    {
      return View();
    }
  }
}
