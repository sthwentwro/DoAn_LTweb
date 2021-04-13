using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doan.Models
{
    public class Giohang
    {
        DataShopDataContext db = new DataShopDataContext();
        public int iIDsanpham { get; set; }
        public string sTensanpham { get; set; }
        public string sAnhSP { get; set;}
        public decimal? dDongia { get; set; }
        public int iSoluong { get; set; }
        public decimal? dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        public Giohang(int IDsanpham,int sl)
        {
            iIDsanpham = IDsanpham;
            Sanpham sp = db.Sanphams.Single(n => n.IDsanpham == IDsanpham);
            sTensanpham = sp.TenSP;
            sAnhSP = sp.AnhSP;
            dDongia = sp.GiaSP;
            iSoluong = sl;
        }
    }
}