using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TokyoFashion.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;
using System.Net;
using System.ComponentModel.DataAnnotations.Schema;

namespace TokyoFashion.Areas.Admin.Controllers
{
    public class TinTucController : Controller
    {
        // GET: Admin/TinTuc
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        public ActionResult DanhSachTT(int? page)
        {
            int pagesize = 6; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<TinTuc> lstTinTuc = db.TinTucs.ToList();
            if (lstTinTuc.Count() == 0)
            {
                ViewBag.lstTinTuc = "Không có sản phẩm thuộc loại này !!!";
            }
            ViewBag.lstTinTuc = db.TinTucs.ToList();
            return View(lstTinTuc.ToPagedList(pagenumber, pagesize));
        }
        
        public ViewResult ChiTietTT(int MaTinTuc)
        {
            TinTuc sp = db.TinTucs.SingleOrDefault(n => n.MaTinTuc == MaTinTuc);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult ThemTinTuc()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemTinTuc([Bind(Include = "MaTinTuc,TieuDe,NoiDung,Anh")] TinTuc TinTuc)
        {
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(TinTuc);
                db.SaveChanges();
                return RedirectToAction("DanhSachTT");
            }
            return View(TinTuc);
        }
        [HttpGet]
        public ActionResult SuaTinTuc(int MaTinTuc)
        {
            if (MaTinTuc == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc TinTuc = db.TinTucs.Find(MaTinTuc);
            if (TinTuc == null)
            {
                return HttpNotFound();
            }
            return View(TinTuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaTinTuc([Bind(Include = "MaTinTuc,TenSP,TenSP,TenThuongHieu,MaMau,Anh,GiaGoc,MoTa,SoLuong,KhuyenMai")] TinTuc TinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(TinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucTT");
            }

            return RedirectToAction("DanhMucTT");
        }
        [HttpGet]
        public ActionResult XoaTinTuc(int MaTinTuc)
        {
            TinTuc TinTuc = db.TinTucs.Single(n => n.MaTinTuc == MaTinTuc);
            if (TinTuc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(TinTuc);
        }

        [HttpPost, ActionName("XoaTinTuc")]
        public ActionResult XacNhanXoa(int MaTinTuc)
        {
            TinTuc TinTuc = db.TinTucs.Single(n => n.MaTinTuc == MaTinTuc);
            db.TinTucs.Remove(TinTuc);
            db.SaveChanges();
            return RedirectToAction("DanhSachSP");
        }
    }
}