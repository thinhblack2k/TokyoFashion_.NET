using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using TokyoFashion.Models;

namespace TokyoFashion.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();

        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangky([Bind(Include = "TenTaiKhoan,MatKhau,HoTen ,Email ,TrangThai,Quyen")] TaiKhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taikhoan);
                db.SaveChanges();
                return View("DangNhap");
            }
            return View(taikhoan);
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(TaiKhoan objUser)
        {
            if (ModelState.IsValid)
            {
                var user = db.TaiKhoans.Where(x => x.TenTaiKhoan.Equals(objUser.TenTaiKhoan) &&
                 x.MatKhau.Equals(objUser.MatKhau) && x.Quyen == 1).FirstOrDefault();
                var admin = db.TaiKhoans.Where(x => x.TenTaiKhoan.Equals(objUser.TenTaiKhoan) &&
                 x.MatKhau.Equals(objUser.MatKhau) && x.Quyen == 1).FirstOrDefault();

                if (admin != null)
                {
                    Session["TenTaiKhoan"] = admin.TenTaiKhoan.ToString();
                    Session["HoTen"] = admin.HoTen.ToString();
                    return View("~/Areas/Admin/Views/Home/Index.cshtml");
                }
                else if (user != null)
                {
                    Session["TenTaiKhoan"] = user.TenTaiKhoan.ToString();
                    Session["HoTen"] = user.HoTen.ToString();
                    return View("~/Views/TrangChu/Index.cshtml");
                }
            }
            return View(objUser);
        }
    }
}