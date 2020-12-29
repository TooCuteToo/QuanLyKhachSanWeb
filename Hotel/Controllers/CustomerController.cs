using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;

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
        public ActionResult Login(FormCollection fc, int id = 0)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (id == 0)
                {
                    string loginEmail = fc["Email"];
                    string loginPass = fc["Password"];

                    KhachHang kh = db.KhachHangs.FirstOrDefault(item => item.email == loginEmail && item.pass == loginPass);

                    if (kh != null)
                    {
                        Session["kh"] = kh;
                        return RedirectToAction("Index", "Home");
                    }

                    return View();
                }

                string regName = fc["Name"];
                string regEmail = fc["Email"];
                string regPass = fc["Password"];

                KhachHang regKh = new KhachHang()
                {
                    tenKH = regName,
                    email = regEmail,
                    pass = regPass,
                    gioiTinh = "Nam",
                };

                db.KhachHangs.InsertOnSubmit(regKh);
                db.SubmitChanges();

                return View();
            }
        }
    }
}
