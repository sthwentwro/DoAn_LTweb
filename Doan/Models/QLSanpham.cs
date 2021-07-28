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
        public List<QLSanpham> getSanphamtheoloai(int loai,int vat)
        {
            DataShopDataContext data = new DataShopDataContext();
            var result = from a in data.Sanphams
                         join b in data.LoaiSPs on a.IDLoaiSP equals b.IDLoaiSP
                         join c in data.LoaiVats on a.IDConvat equals c.IDConvat
                         where a.IDLoaiSP == loai && a.IDConvat==vat
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
        public bool CreateSP(Sanpham pro)
        {
            try
            {
                DataShopDataContext data = new DataShopDataContext();
                data.Sanphams.InsertOnSubmit(pro);
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool EditSP(Sanpham pro)
        {
            try
            {
                DataShopDataContext data = new DataShopDataContext();
                var sp = data.Sanphams.Where(a => a.IDsanpham == pro.IDsanpham).FirstOrDefault();
                sp.TenSP = pro.TenSP;
                sp.GiaSP = pro.GiaSP;
                sp.TrongLuong = pro.TrongLuong;
                sp.NSX = pro.NSX;
                sp.DVT = pro.DVT;
                sp.AnhSP = pro.AnhSP;
                sp.SLton = pro.SLton;
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }          
        }
        public bool DeleteSP(int id)
        {
            try
            {
                DataShopDataContext data = new DataShopDataContext();
                Sanpham sp = data.Sanphams.First(n => n.IDsanpham == id);
                data.Sanphams.DeleteOnSubmit(sp);
                data.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Sanpham get1Sanpham(int ID)
        {
            DataShopDataContext data = new DataShopDataContext();
            return data.Sanphams.Where(a => a.IDsanpham == ID).FirstOrDefault();
        }
    }
}