using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doan.Models
{
    public class QLdondathang
    {
        DataShopDataContext db = new DataShopDataContext();

        public int IDdondathang { get; set; }
        public int? IDnguoidung { get; set; }
        public int? Tongsl { get; set; }
        public decimal? Tonggiatri { get; set; }
        public DateTime? Ngaydat { get; set; }
        public DateTime? Ngaygiao { get; set; }
        public bool? Tinhtrang { get; set; }

        public List<QLdondathang> dsDDH()
        {
            var sql = from a in db.ChitietHoadons
                      group a by a.IDHoadon into pg
                      join b in db.HoaDons on pg.FirstOrDefault().IDHoadon equals b.IDHoadon
                      let Tonggiatri = pg.Sum(x => x.Soluong)* pg.FirstOrDefault().Dongia
                      select new QLdondathang()
                      {
                          IDdondathang = pg.FirstOrDefault().IDHoadon,
                          IDnguoidung = b.IDnguoidung,
                          Tongsl = pg.Sum(x=>x.Soluong),
                          Tonggiatri = Tonggiatri,
                          Ngaydat = b.Ngaydat,
                          Ngaygiao = b.Ngaygiao,
                          Tinhtrang = b.TinhTrangGiaohang
                      };
            return sql.ToList();
        }
        public HoaDon CTHD(int IDdondathang)
        {
            return db.HoaDons.FirstOrDefault(n=>n.IDHoadon== IDdondathang);
        }
        public bool UpdateHD(HoaDon don)
        {
            try
            {
                var hd = db.HoaDons.FirstOrDefault(n => n.IDHoadon == don.IDHoadon);
                hd.IDnguoidung = don.IDnguoidung;
                hd.Ngaydat = don.Ngaydat;
                hd.Ngaygiao = don.Ngaygiao;
                hd.TinhTrangGiaohang = don.TinhTrangGiaohang;
                hd.DaThanhToan = don.DaThanhToan;
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }          
        }
        public bool RemoveHD(int id)
        {
            try
            {
                var cthd = db.ChitietHoadons.Where(n=>n.IDHoadon==id).ToList();
                var hd = db.HoaDons.FirstOrDefault(n => n.IDHoadon == id);
                foreach (var item in cthd)
                {
                    db.ChitietHoadons.DeleteOnSubmit(item);                  
                }
                db.HoaDons.DeleteOnSubmit(hd);
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