using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;

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
        public ActionResult ThemMatHang( FormCollection fc, string tenPhong)
        {
            KhachHang kh = Session["kh"] as KhachHang;

            if (kh == null)
            {
                return RedirectToAction("Login", "Customer", new { id = 0 });
            }

            Cart cart = Session["cart"] as Cart;

            if (cart == null)
            {
                cart = new Cart();
            }

            Room phong  = new Room(tenPhong);

            phong.ngayDat = fc["ngayDat"] != null ? DateTime.Parse(fc["ngayDat"]) : DateTime.Now;
            phong.ngayTra = fc["ngayTra"] != null ? DateTime.Parse(fc["ngayTra"]) : DateTime.Now;

            phong.inStock = true;
            cart.add(phong);

            Session["cart"] = cart;

            return RedirectToAction("Index", "Home");
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
        public ActionResult EditMatHang(FormCollection fc, string tenPhong)
        {
            Cart cart = Session["cart"] as Cart;
            Room phong = cart.items.FirstOrDefault(item => item.tenPhong == tenPhong);

            phong.ngayDat = fc["ngayDat"] != null ? DateTime.Parse(fc["ngayDat"]) : DateTime.Now;
            phong.ngayTra = fc["ngayTra"] != null ? DateTime.Parse(fc["ngayTra"]) : DateTime.Now;

            Session["cart"] = cart;

            return RedirectToAction("ChiTiet", "DatHang");
        }

        public ActionResult ThanhToanDonHang(bool thanhToan)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                KhachHang kh = Session["kh"] as KhachHang;

                if (thanhToan == true)
                {
                    Cart cart = Session["cart"] as Cart;
                    
                    ViewBag.kh = kh;
                    ViewBag.thanhToan = true;

                    return View(cart);
                }


                if (kh == null)
                {
                    return RedirectToAction("Login", "Customer", new { id = 0 });
                }

                List<HoaDon> dsHoaDon = db.HoaDons.Where(item => item.email == kh.email).ToList();

                ViewBag.kh = kh;
                ViewBag.thanhToan = false;

                ViewBag.hoaDons = dsHoaDon;
                ViewBag.soLuong = dsHoaDon.Count;

                ViewBag.tongTien = dsHoaDon.Sum(item => item.tienThanhToan);
                

                return View();
            }
        }

        [HttpPost]
        public ActionResult ThanhToanDonHang()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                Cart cart = Session["cart"] as Cart;
                KhachHang kh = Session["kh"] as KhachHang;

                foreach (Room phong in cart.items)
                {
                    int totalDays = phong.ngayTra.Subtract(phong.ngayDat).Days;
                    string rightPart = (db.HoaDons.Count() + 1).ToString().PadLeft(2, '0');

                    HoaDon hd = new HoaDon()
                    {
                        maHD = "HD" + rightPart,
                        email = kh.email,
                        ngayDat = phong.ngayDat,
                        ngayTra = phong.ngayTra,
                        tenPhong = phong.tenPhong,
                        tienThanhToan = phong.giamGia != null ? phong.giamGia * totalDays : phong.giaPhong * totalDays,
                    };

                    db.Phongs.FirstOrDefault(item => item.tenPhong == phong.tenPhong).tinhTrang = "Đặt";

                    db.HoaDons.InsertOnSubmit(hd);
                    db.SubmitChanges();

                    ViewBag.kh = kh;
                }

                Session["cart"] = null;

                return RedirectToAction("ThanhToanDonHang", "DatHang", new { thanhToan = false });
            }
        }

        [HttpGet]
        public ActionResult XoaDonHang(string tenPhong, bool thanhToan)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                Cart cart = Session["cart"] as Cart;
                KhachHang kh = Session["kh"] as KhachHang;

                if (thanhToan == true)
                {
                    Room phong = new Room(tenPhong);
                    cart.delete(phong);
                    return RedirectToAction("ThanhToanDonHang", "DatHang", new { thanhToan = true });
                }

                HoaDon hd = db.HoaDons.FirstOrDefault(item => item.tenPhong == tenPhong);
                db.Phongs.FirstOrDefault(item => item.tenPhong == tenPhong).tinhTrang = "Trống";

                db.HoaDons.DeleteOnSubmit(hd);
                db.SubmitChanges();

                return RedirectToAction("ThanhToanDonHang", "DatHang", new { thanhToan = false });
            }
        }
        
    }
}
