using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class DetailController : Controller
    {
        //
        // GET: /Detail/

        public ActionResult Index(string id)
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
                                           tenLP = lp.tenLP,
                                           tinhTrang = ph.tinhTrang,
                                       };

                Room phong = tList.ToList().FirstOrDefault(item => item.tenPhong.Contains(id));

                ViewBag.suggestRoom = tList.Where(item => item.maLoai != phong.maLoai).Skip(tList.Count() - tList.Count() / 2).ToList();

                Cart cart = Session["cart"] as Cart;

                if (cart != null)
                {
                    Room room = cart.items.FirstOrDefault(item => item.tenPhong == id);

                    if (room != null)
                    {
                        phong.inStock = true;
                    }
                }

                return View("Index", phong);
            }
        }

        //public ActionResult Index(Room val)
        //{
        //    using (DataClasses1DataContext db = new DataClasses1DataContext())
        //    {
        //        var tList = from ph in db.Phongs
        //                    join lp in db.LoaiPhongs
        //                    on ph.maLoai equals lp.maLoai
        //                    select new Room
        //                    {
        //                        danhGia = ph.danhGia,
        //                        tenPhong = ph.tenPhong,
        //                        hinhAnh = ph.hinhAnh,
        //                        giamGia = ph.giamGia,
        //                        giaPhong = lp.giaPhong,
        //                        maLoai = lp.maLoai,
        //                        tenLP = lp.tenLP,
        //                    };

        //        Room phong = tList.ToList().FirstOrDefault(item => item.tenPhong == val.tenPhong);

        //        ViewBag.suggestRoom = tList.Where(item => item.maLoai != phong.maLoai).Skip(tList.Count() - tList.Count() / 2).ToList();

        //        Cart cart = Session["cart"] as Cart;

        //        ViewBag.inStock = false;

        //        if (cart != null)
        //        {
        //            Room room = cart.items.FirstOrDefault(item => item.tenPhong == val.tenPhong);
        //            ViewBag.inStock = room == null ? false : true;
        //        }

        //        return View("Index", phong);
        //    }
        //}

    }
}
