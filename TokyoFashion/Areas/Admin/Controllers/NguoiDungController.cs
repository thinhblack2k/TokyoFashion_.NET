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
    public class NguoiDungController : Controller
    {
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        // GET: Admin/NguoiDung
        public ActionResult DanhSachNguoiDung(int? page)
        {
            int pagesize = 6; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<TaiKhoan> lstTaiKhoan = db.TaiKhoans.ToList();
            if (lstTaiKhoan.Count() == 0)
            {
                ViewBag.lstTaiKhoan = "Không có sản phẩm thuộc loại này !!!";
            }
            ViewBag.lstTaiKhoan = db.TaiKhoans.ToList();
            return View(lstTaiKhoan.ToPagedList(pagenumber, pagesize));
        }
    }
}