using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TokyoFashion.Models;
using PagedList;
using System.Data.Entity;
using System.Net;

namespace TokyoFashion.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        // GET: Admin/DonHang
        public ActionResult DanhSachDonHang(int? page)
        {
            int pagesize = 6; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<DonHang> lstDonHang = db.DonHangs.ToList();
            ViewBag.lstDonHang = db.DonHangs.ToList();
            return View(lstDonHang.ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public ActionResult SuaDH(string SoHDB)
        {
            if (SoHDB == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang hdb = db.DonHangs.Find(SoHDB);
            if (hdb == null)
            {
                return HttpNotFound();
            }
            return View(hdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaDH([Bind(Include = "MaDonHang,MaKH,NgayDatHang,TrangThai,TongTien")] DonHang hdb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hdb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachDonHang");
            }

            return RedirectToAction("DanhSachDonHang");
        }
        [HttpGet]
        public ActionResult XoaDH(int MaDonHang)
        {
            DonHang hdb = db.DonHangs.Single(n => n.MaDonHang == MaDonHang);
            if (hdb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hdb);
        }

        [HttpPost, ActionName("XoaDH")]
        public ActionResult XacNhanXoa(int MaDonHang)
        {
            DonHang hdb = db.DonHangs.Single(n => n.MaDonHang == MaDonHang);
            db.DonHangs.Remove(hdb);
            db.SaveChanges();
            return RedirectToAction("DanhSachDonHang");
        }

        

        public ActionResult ChitietHD(int MaDonHang)
        {
            double total = 0;
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);
            total = (double)dh.ChiTietDonHangs.Sum(n => n.GiaBan * n.SoLuongSP);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.total = total;
            return View(dh);
        }
    }
}