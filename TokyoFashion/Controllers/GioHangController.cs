using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TokyoFashion.Models;
using TokyoFashion.Models.Datas;

namespace TokyoFashion.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int sMaSP, string strUrl)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang spgh = lstGioHang.Find(n => n.sMaSP == sMaSP);
            if (spgh == null)
            {
                spgh = new GioHang(sMaSP);
                lstGioHang.Add(spgh);
                return Redirect(strUrl);
            }
            else
            {
                spgh.iSoLuong++;
                return Redirect(strUrl);
            }
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "TrangChu");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "TrangChu");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        private int TongSoLuong()
        {
            int iTongSL = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSL = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSL;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() != 0)
            {
                ViewBag.TongSoLuong = TongSoLuong();
            }
            return PartialView();
        }
       
        public ActionResult CapNhatGioHang(int sMaSP, FormCollection f)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sản phẩm có trong giỏ hàng không
            GioHang sp = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("SuaGioHang");
        }
        public ActionResult XoaGioHang(int sMaSP, FormCollection f)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sản phẩm có trong giỏ hàng không
            GioHang sp = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.sMaSP == sMaSP);
            }
            if (lstGioHang.Count == 0)
            {
                RedirectToAction("Index", "TrangChu");
            }
            return RedirectToAction("SuaGioHang");
        }

        //Đặt hàng
        
    }
}