//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TokyoFashion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhanHoi
    {
        public int MaPH { get; set; }
        public Nullable<int> MaSP { get; set; }
        public string TenNguoiPH { get; set; }
        public string NoidungPH { get; set; }
        public Nullable<System.DateTime> ThoiGianPH { get; set; }
    
        public virtual SanPham SanPham { get; set; }
    }
}
