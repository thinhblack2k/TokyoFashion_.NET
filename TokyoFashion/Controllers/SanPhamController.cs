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
    public class SanPhamController : Controller
    {
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        // GET: SanPham
        public ViewResult ChitietSP(int MaSP = 1)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        public ViewResult ChitietSP2(int MaSP = 1)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        public ActionResult SanPhamTuongTu(int? page)
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



        [HttpPost]
        public ActionResult resultsSearch(FormCollection f, int? page)
        {
            string searchkey = f["txtsearchkey"].ToString();
            ViewBag.keyword = searchkey;
            List<SanPham> lstKQ = db.SanPhams.Where(n => n.TenSP.Contains(searchkey)).ToList();
            int pagenumber = (page ?? 1);
            int pagesize = 12;
            if (lstKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có sản phẩm bạn đang tìm kiếm !";
                return View(db.SanPhams.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy " + lstKQ.Count + " sản phẩm ";
            return View(lstKQ.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
        }

        [HttpGet]
        public ActionResult resultsSearch(int? page, string searchkey)
        {
            ViewBag.keyword = searchkey;
            List<SanPham> lstKQ = db.SanPhams.Where(n => n.TenSP.Contains(searchkey)).ToList();
            int pagenumber = (page ?? 1);
            int pagesize = 12;
            if (lstKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có sản phẩm bạn đang tìm kiếm !";
                return View(db.SanPhams.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
            }
            ViewBag.Thongbao = "Đã tìm thấy " + lstKQ.Count + " sản phẩm ";
            return View(lstKQ.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
        }



    }
}