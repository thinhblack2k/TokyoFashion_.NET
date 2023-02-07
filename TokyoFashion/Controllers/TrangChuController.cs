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
    public class TrangChuController : Controller
    {
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        // GET: TrangChu
        public ActionResult Index(int? page)
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


        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult DanhSachYeuThich(int? page)
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

        public ActionResult Loi()
        {
            return View();
        }

        public PartialViewResult ListTinTuc()
        {
            return PartialView(db.TinTucs.ToList());
        }

        public PartialViewResult ListPhanHoi()
        {
            return PartialView(db.PhanHois.ToList());
        }

        public ActionResult Tintuc()
        {
            return View();
        }

        public ActionResult DanhSachTinTuc(int? page)
        {
            int pagesize = 12; // so san pham tren 1 trang
            int pagenumber = (page ?? 1); //so trang 
            List<TinTuc> lstTinTuc = db.TinTucs.ToList();
            if (lstTinTuc.Count() == 0)
            {
                ViewBag.lstTinTuc = "Không có sản phẩm thuộc loại này!";
            }
            ViewBag.lstTinTuc = db.TinTucs.ToList();
            return View(lstTinTuc.ToPagedList(pagenumber, pagesize));
        }

        public ViewResult SPTheoDanhMucVi(int? page, int MaDanhMuc = 3)
        {
            int pagesize = 12; // so san pham tren 1 trang
            int pagenumber = (page ?? 1); //so trang 

            DanhMucSP lsp = db.DanhMucSPs.SingleOrDefault(n => n.MaDanhMuc == 3);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SanPham> lstSanPham = db.SanPhams.Where(m => m.MaDanhMuc == 3).OrderBy
                (m => m.MaDanhMuc).ToList();
            if (lstSanPham.Count() == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm thuộc loại này!";
            }
            ViewBag.lstSanPham = db.SanPhams.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }
    }
}