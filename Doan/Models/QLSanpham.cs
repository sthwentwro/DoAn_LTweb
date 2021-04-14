using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doan.Models
{
    public class QLSanpham
    {
        public int IDsanpham { set;get; }
        public string TenSP { set; get; }
        public decimal? GiaSP { set; get; }
        public string cover { set; get; }
        public string mota { set; get; }
        public int? soluong { set; get; }
        public string NSX { set; get; }
        public string trongluong { set; get; }
        public string DVT { set; get; }
        public string Tenconvat { set; get; }
        public  string loaiSP { set; get; }
    }
    class ListSP
    {
        public List<Sanpham> listSanpham()
        {
            DataShopDataContext data = new DataShopDataContext();
            return data.Sanphams.ToList();
        }
            public List<QLSanpham> getSanpham(int ID)
        {
            DataShopDataContext data = new DataShopDataContext();
            var result = from a in data.Sanphams
                         join b in data.LoaiSPs on a.IDLoaiSP equals b.IDLoaiSP
                         join c in data.LoaiVats on a.IDConvat equals c.IDConvat
                         where a.IDsanpham == ID
                         select new QLSanpham()
                         {
                             IDsanpham = a.IDsanpham,
                             TenSP = a.TenSP,
                             GiaSP = a.GiaSP,
                             cover = a.AnhSP,
                             mota = a.MoTa,
                             soluong = a.SLton,
                             NSX = a.NSX,
                             trongluong = a.TrongLuong,
                             DVT = a.DVT,
                             Tenconvat = c.TenConVat,
                             loaiSP = b.TenLoai,
                         };
            return result.ToList();
        }
        public List<QLSanpham> getSanphamtheoloai(int loai)
        {
            DataShopDataContext data = new DataShopDataContext();
            var result = from a in data.Sanphams
                         join b in data.LoaiSPs on a.IDLoaiSP equals b.IDLoaiSP
                         join c in data.LoaiVats on a.IDConvat equals c.IDConvat
                         where a.IDLoaiSP == loai
                         select new QLSanpham()
                         {
                             IDsanpham = a.IDsanpham,
                             TenSP = a.TenSP,
                             GiaSP = a.GiaSP,
                             cover = a.AnhSP,
                             mota = a.MoTa,
                             soluong = a.SLton,
                             NSX = a.NSX,
                             trongluong = a.TrongLuong,
                             DVT = a.DVT,
                             Tenconvat = c.TenConVat,
                             loaiSP = b.TenLoai,
                         };
            return result.ToList();
        }
        public List<Sanpham> getSanphamhot(int count)
        {
            DataShopDataContext data = new DataShopDataContext();
            return data.Sanphams.Take(count).ToList();
        }
    }
}