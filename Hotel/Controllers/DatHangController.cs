using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;
using Hotel.Helpers;

namespace Hotel.Controllers
{
  public class DatHangController : Controller
  {
    //
    // GET: /DatHang/

    public ActionResult ChiTiet()
    {
      KhachHang kh = Session["kh"] as KhachHang;

      if (kh == null)
      {
        return RedirectToAction("Login", "Customer", new { id = 0 });
      }

      Cart cart = Session["cart"] as Cart;
      ViewBag.DichVus = DBDichVu.getDichVus();

      return View("ChiTiet", cart);
    }

    public ActionResult KhoiTaoIconGiohang()
    {
      Cart cart = Session["cart"] as Cart;

      if (cart == null)
      {
        return PartialView(0);
      }

      return PartialView(cart.count());
    }

    [HttpPost]
    public JsonResult ThemMatHang(HoaDon hd, string[] maDichVu, string tenPhong)
    {
      KhachHang kh = Session["kh"] as KhachHang;

      if (kh == null)
      {
        return Json(new { message = "/Customer/Login/0" });
      }

      Cart cart = Session["cart"] as Cart;

      if (cart == null)
      {
        cart = new Cart();
      }

      Room phong = new Room(tenPhong);

      phong.ngayDat = DateTime.Parse(hd.ngayDat.ToString());
      phong.ngayTra = DateTime.Parse(hd.ngayTra.ToString());

      if (maDichVu == null) maDichVu = new string[] { "6" };

      phong.maDichVus.AddRange(maDichVu);

      phong.inStock = true;
      cart.add(phong);

      Session["cart"] = cart;

      return Json(new { message = "/DatHang/ChiTiet", kq = phong.maDichVus });
    }

    public ActionResult XoaMatHang(string tenPhong)
    {
      Cart cart = Session["cart"] as Cart;

      Room phong = new Room(tenPhong);
      phong.inStock = false;

      cart.delete(phong);

      Session["cart"] = cart;

      return RedirectToAction("ChiTiet", "DatHang");
    }

    [HttpPost]
    public JsonResult EditMatHang(Room room, List<string> maDichVus)
    {
      Cart cart = Session["cart"] as Cart;
      Room phong = cart.items.FirstOrDefault(item => item.tenPhong == room.tenPhong);

      if (maDichVus == null) maDichVus = new List<string>() { "6" };

      phong.maDichVus = new List<string>();
      phong.maDichVus.AddRange(maDichVus);

      phong.ngayDat = room.ngayDat;
      phong.ngayTra = room.ngayTra;

      Session["cart"] = cart;

      return Json(new { message = "/DatHang/ChiTiet" });
    }

    [HttpPost]
    public JsonResult GetServicesDetail(string tenPhong)
    {
      Cart cart = Session["cart"] as Cart;

      if (cart != null)
      {
        Room phong = cart.items.FirstOrDefault(item => item.tenPhong == tenPhong);
        return Json(new { maDichVus = phong.maDichVus });
      }

      List<string> maDichVus = new List<string>();
      HoaDon hd = DBHoaDon.getHoaDon(tenPhong);
      List<ChiTietDichVu> dichVus = DBDichVu.getDichVusByMa(hd.maHD);

      foreach (ChiTietDichVu ct in dichVus)
      {
        maDichVus.Add(ct.maDichVu.ToString());
      }

      return Json(new { maDichVus });
    }

    public ActionResult ThanhToanDonHang(bool thanhToan)
    {
      KhachHang kh = Session["kh"] as KhachHang;

      if (thanhToan == true)
      {
        Cart cart = Session["cart"] as Cart;

        ViewBag.kh = kh;
        ViewBag.thanhToan = true;
        ViewBag.dichVus = DBDichVu.getDichVus();

        return View(cart);
      }

      if (kh == null)
      {
        return RedirectToAction("Login", "Customer", new { id = 0 });
      }

      List<HoaDon> dsHoaDon = DBHoaDon.getHoaDons(kh);

      ViewBag.kh = kh;
      ViewBag.thanhToan = false;

      ViewBag.hoaDons = dsHoaDon;
      ViewBag.soLuong = dsHoaDon.Count;
      ViewBag.dichVus = DBDichVu.getDichVus();

      ViewBag.tongTien = dsHoaDon.Sum(item => item.tienThanhToan);

      return View();
    }

    [HttpPost]
    public ActionResult ThanhToanDonHang()
    {
      Cart cart = Session["cart"] as Cart;
      KhachHang kh = Session["kh"] as KhachHang;

      DBHoaDon.createHoaDon(cart.items, kh);

      foreach (Room phong in cart.items)
      {
        DBDichVu.createServicesDetail(phong.tenPhong, phong.maDichVus);
      }
    
      ViewBag.kh = kh;
      Session["cart"] = null;

      return RedirectToAction("ThanhToanDonHang", "DatHang", new { thanhToan = false });
    }

    [HttpGet]
    public ActionResult XoaDonHang(string tenPhong, bool thanhToan)
    {
      Cart cart = Session["cart"] as Cart;
      KhachHang kh = Session["kh"] as KhachHang;

      if (thanhToan == true)
      {
        Room phong = new Room(tenPhong);
        cart.delete(phong);
        return RedirectToAction("ThanhToanDonHang", "DatHang", new { thanhToan = true });
      }

      HoaDon hd = DBHoaDon.getHoaDon(tenPhong);

      DBDichVu.removeServicesDetail(hd);

      DBHoaDon.removeHoaDon(tenPhong);

      return RedirectToAction("ThanhToanDonHang", "DatHang", new { thanhToan = false });
    }

  }
}
