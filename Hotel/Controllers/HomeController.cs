using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;

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
                               tenLP = lp.tenLP,
                               maLoai = lp.maLoai,
                               tinhTrang = ph.tinhTrang,
                           };

                List<Room> listPhong = tList.OrderBy(item => new Random().Next()).ToList();

                Cart cart = (Cart) Session["cart"];

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
                    if (maLoai.Contains("LP"))
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
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (maLoai != null)
                {
                    if (maLoai == "0")
                    {
                        ViewBag.tenLP = "Tất cả phòng";
                        return View("Room", (object)maLoai);
                    }

                    ViewBag.tenLP = db.LoaiPhongs.FirstOrDefault(item => item.maLoai == maLoai).tenLP;
                    ViewBag.tenPhong = tenPhong.ToLower();

                    return View("Room", (object)maLoai);
                }
            }

            return View("Room", (object)maLoai);

        }

        [HttpPost]
        public ActionResult Room(FormCollection fc)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                string tenPhong = fc["tenPhong"];
                string loaiPhong = fc["loaiPhong"];

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
                               tenLP = lp.tenLP,
                               maLoai = lp.maLoai,
                           };

                return RedirectToAction("Room", "Home", new { maLoai = loaiPhong, tenPhong = tenPhong });
            }
        }

        public ActionResult KhoiTaoSubMenu()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                try
                {
                    List<LoaiPhong> listLP = db.LoaiPhongs.ToList();

                    return PartialView(listLP);
                }
                catch
                {
                    return PartialView();
                }
            }
        }
    }
}
