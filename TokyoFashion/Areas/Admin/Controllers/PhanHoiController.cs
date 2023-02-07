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
    public class PhanHoiController : Controller
    {
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        // GET: Admin/PhanHoi
        public ActionResult DanhSachPH(int? page)
        {
            int pagesize = 3; // so san pham tren 1 trang
            int pagenumber = (page ?? 1);
            List<PhanHoi> lstPhanHoi = db.PhanHois.ToList();
            if (lstPhanHoi.Count() == 0)
                ViewBag.lstPhanHoi = db.PhanHois.ToList();
            return View(lstPhanHoi.ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public ActionResult XoaPH(int MaPH)
        {
            PhanHoi ph = db.PhanHois.Single(n => n.MaPH == MaPH);
            if (ph == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ph);
        }

        [HttpPost, ActionName("XoaPH")]
        public ActionResult XacNhanXoa(int MaPH)
        {
            PhanHoi ph = db.PhanHois.Single(n => n.MaPH == MaPH);
            db.PhanHois.Remove(ph);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}