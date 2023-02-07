using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TokyoFashion.Models.Datas;
using TokyoFashion.Models;
using PagedList;
using System.Net;
using System.Data.Entity;

namespace TokyoFashion.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        public ActionResult DanhSachKH(int? page)
        {
            int pagesize = 6; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<KhachHang> lstKH = db.KhachHangs.ToList();
            if (lstKH.Count() == 0)
                ViewBag.lstKH = db.KhachHangs.ToList();
            return View(lstKH.ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public ActionResult SuaKH(string MaKH)
        {
            if (MaKH == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang kh = db.KhachHangs.Find(MaKH);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaKH([Bind(Include = "MaKH,TenKH,Mail,DienThoai")] KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaKH(int MaKH)
        {
            KhachHang kh = db.KhachHangs.Single(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost, ActionName("XoaKH")]
        public ActionResult XacNhanXoa(int MaKH)
        {
            KhachHang kh = db.KhachHangs.Single(n => n.MaKH == MaKH);
            db.KhachHangs.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("DanhSachKH");
        }
    }
}