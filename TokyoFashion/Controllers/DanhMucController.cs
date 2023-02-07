using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TokyoFashion.Models;
using System.Net;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;


namespace TokyoFashion.Controllers
{
    public class DanhMucController : Controller
    {
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        // GET: DanhMuc
        public ActionResult SanPhamCuaHang(int? page)
        {
            int pagesize = 12; // so san pham tren 1 trang
            int pagenumber = (page ?? 1); //so trang 
            List<SanPham> lstSanPham = db.SanPhams.ToList();
            if (lstSanPham.Count() == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này!";
            }
            ViewBag.lstSanPham = db.SanPhams.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }


        // List Danh Mục Sản Phẩm
        public PartialViewResult DanhMucPartial()
        {
            return PartialView(db.DanhMucSPs.ToList());
        }
        public PartialViewResult DanhMucPartial2()
        {
            return PartialView(db.DanhMucSPs.ToList());
        }
        public PartialViewResult DanhMucPartial3()
        {
            return PartialView(db.DanhMucSPs.ToList());
        }


        public ViewResult SPTheoDanhMuc(int? page, int MaDanhMuc = 3)
        {
            int pagesize = 12; // so san pham tren 1 trang
            int pagenumber = (page ?? 1); //so trang 

            DanhMucSP lsp = db.DanhMucSPs.SingleOrDefault(n => n.MaDanhMuc == MaDanhMuc);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SanPham> lstSanPham = db.SanPhams.Where(m => m.MaDanhMuc == MaDanhMuc).OrderBy
                (m => m.MaDanhMuc).ToList();
            if (lstSanPham.Count() == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này!";
            }
            ViewBag.lstSanPham = db.SanPhams.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }

        public PartialViewResult DanhMucLeft()
        {
            return PartialView(db.DanhMucSPs.ToList());
        }

        public PartialViewResult DanhMucMauSac()
        {
            return PartialView(db.MauSacs.ToList());
        }
    }
}