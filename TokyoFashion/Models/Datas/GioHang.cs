using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokyoFashion.Models.Datas
{
    public class GioHang
    {
        WebTokyoFashionEntities db = new WebTokyoFashionEntities();
        public int sMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhSP { get; set; }
        public double dGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return dGia * iSoLuong; }
        }
        public GioHang(int MaSP)
        {
            sMaSP = MaSP;
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            sTenSP = sanpham.TenSP;
            sAnhSP = sanpham.Anh;
            dGia = double.Parse(sanpham.GiaGoc.ToString());
            iSoLuong = 1;
        }
    }
}