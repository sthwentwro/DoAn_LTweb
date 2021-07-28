using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doan.Models
{
    public class Giohang
    {
        DataShopDataContext db = new DataShopDataContext();
        public int? iIDsanpham { get; set; }
        public string sTensanpham { get; set; }
        public string sAnhSP { get; set;}
        public decimal? dDongia { get; set; }
        public int iSoluong { get; set; }
        public decimal? dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        public Giohang(int? IDsanpham,int sl)
        {
            iIDsanpham = IDsanpham;
            Sanpham sp = db.Sanphams.Single(n => n.IDsanpham == IDsanpham);
            sTensanpham = sp.TenSP;
            sAnhSP = sp.AnhSP;
            dDongia = sp.GiaSP;
            iSoluong = sl;
        }
        public Giohang()
        {
        }
        public bool Dathang(Nguoidung user,List<Giohang> gh,string ngaygiao)
        {
            try
            {
                HoaDon ddh = new HoaDon();
                ddh.IDnguoidung = 1;
                ddh.Ngaydat = DateTime.Now;
                ddh.Ngaygiao = DateTime.Parse(ngaygiao);
                ddh.TinhTrangGiaohang = false;
                ddh.DaThanhToan = false;
                db.HoaDons.InsertOnSubmit(ddh);
                db.SubmitChanges();
                foreach (var item in gh)
                {
                    ChitietHoadon ctdh = new ChitietHoadon();
                    ctdh.IDHoadon = ddh.IDHoadon;
                    ctdh.IDsanpham = (int)item.iIDsanpham;
                    ctdh.Soluong = item.iSoluong;
                    ctdh.Dongia = (decimal)item.dDongia;
                    db.ChitietHoadons.InsertOnSubmit(ctdh);
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }           
        }
    }
}