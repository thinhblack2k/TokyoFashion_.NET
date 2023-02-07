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
    public class SanPhamAdController : Controller
    {
        // GET: Admin/SanPham
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        public ActionResult DanhSachSP(int? page)
        {
            int pagesize = 7; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<SanPham> lstSanPham = db.SanPhams.ToList();
            if (lstSanPham.Count() == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này !!!";
            }
            ViewBag.lstSanPham = db.SanPhams.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }
        public ActionResult DanhMucSP(int? page)
        {
            int pagesize = 6; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<DanhMucSP> lstDanhMucSP = db.DanhMucSPs.ToList();
            if (lstDanhMucSP.Count() == 0)
            {
                ViewBag.lstDanhMucSP = "Không có sản phẩm thuộc loại này !!!";
            }
            ViewBag.lstDanhMucSP = db.DanhMucSPs.ToList();
            return View(lstDanhMucSP.ToPagedList(pagenumber, pagesize));
        }
        public ActionResult MauSac(int? page)
        {
            int pagesize = 6; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<MauSac> lstMauSac = db.MauSacs.ToList();
            if (lstMauSac.Count() == 0)
            {
                ViewBag.lstMauSac = "Không có sản phẩm thuộc loại này !!!";
            }
            ViewBag.lstMauSac = db.MauSacs.ToList();
            return View(lstMauSac.ToPagedList(pagenumber, pagesize));
        }
        public ActionResult KichThuoc(int? page)
        {
            int pagesize = 6; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<KichThuoc> lstKichThuoc = db.KichThuocs.ToList();
            if (lstKichThuoc.Count() == 0)
            {
                ViewBag.lstKichThuoc = "Không có sản phẩm thuộc loại này !!!";
            }
            ViewBag.lstKichThuoc = db.KichThuocs.ToList();
            return View(lstKichThuoc.ToPagedList(pagenumber, pagesize));
        }
        public PartialViewResult DanhMucPartial()
        {
            return PartialView(db.DanhMucSPs.ToList());
        }
        public PartialViewResult MauPartial()
        {
            return PartialView(db.MauSacs.ToList());
        }
        public PartialViewResult DungTichPartial()
        {
            return PartialView(db.KichThuocs.ToList());
        }
        public ViewResult ChiTietSP(int MaSP)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult ThemSanPham()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.MaDanhMuc), "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaMau = new SelectList(db.MauSacs.ToList().OrderBy(n => n.MaMau), "MaMau", "TenMau");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPham([Bind(Include = "MaDanhMuc,TenSP,TenThuongHieu,MaMau,Anh,GiaGoc,MoTa,Soluong,KhuyenMai")] SanPham SanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(SanPham);
                db.SaveChanges();
                return RedirectToAction("DanhSachSP");
            }
            return View(SanPham);
        }
        [HttpGet]
        public ActionResult SuaSanPham(int MaSP)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanpham = db.SanPhams.Find(MaSP);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucSPs.ToList().OrderBy(n => n.MaDanhMuc), "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaMau = new SelectList(db.MauSacs.ToList().OrderBy(n => n.MaMau), "MaMau", "TenMau");
            
            return View(sanpham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSanPham([Bind(Include = "MaSP,TenSP,TenSP,TenThuongHieu,MaMau,Anh,GiaGoc,MoTa,SoLuong,KhuyenMai")] SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachSP");
            }

            return RedirectToAction("DanhSachSP");
        }
        [HttpGet]
        public ActionResult XoaSanPham(int MaSP)
        {
            SanPham sanpham = db.SanPhams.Single(n => n.MaSP == MaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }

        [HttpPost, ActionName("XoaSanPham")]
        public ActionResult XacNhanXoa(int MaSP)
        {
            SanPham sanpham = db.SanPhams.Single(n => n.MaSP == MaSP);
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("DanhSachSP");
        }

        [HttpGet]
        public ActionResult ThemDanhMuc()
        {
          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDanhMuc([Bind(Include = "MaDanhMuc,TenDanhMuc")] DanhMucSP danhmuc)

        {
            if (ModelState.IsValid)
            {
                db.DanhMucSPs.Add(danhmuc);
                db.SaveChanges();
                return RedirectToAction("DanhMucSP");
            }
            return View(danhmuc);
        }

        [HttpGet]
        public ActionResult XoaDanhMuc(int MaDanhMuc)
        {
            DanhMucSP danhmuc = db.DanhMucSPs.Single(n => n.MaDanhMuc == MaDanhMuc);
            if (danhmuc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(danhmuc);
        }

        [HttpPost, ActionName("XoaDanhMuc")]
        public ActionResult XacNhanXoaDM(int MaDanhMuc)
        {
            DanhMucSP danhmuc = db.DanhMucSPs.Single(n => n.MaDanhMuc == MaDanhMuc);
            db.DanhMucSPs.Remove(danhmuc);
            db.SaveChanges();
            return RedirectToAction("DanhMucSP");
        }


    }

}