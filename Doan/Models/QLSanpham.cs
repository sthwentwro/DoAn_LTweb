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
        public decimal GiaSP { set; get; }
        public string cover { set; get; }
        public string mota { set; get; }
        public int soluong { set; get; }
        public string NSX { set; get; }
        public string trongluong { set; get; }
        public string DVT { set; get; }
        public int IDconvat { set; get; }
        public int IDloaiSP { set; get; }
    }
    class ListSP
    {
        public List<Sanpham> getSanpham(int ? ID)
        {
            DataShopDataContext data = new DataShopDataContext();
            if (ID.Equals(null))
            {
                return data.Sanphams.ToList();
            }
            return data.Sanphams.Where(i => i.IDsanpham == ID).ToList();
        }
        public List<Sanpham> getSanphamtheoloai(int loai)
        {
            DataShopDataContext data = new DataShopDataContext();
            return data.Sanphams.Where(i => i.IDLoaiSP == loai).ToList();
        }
        public List<Sanpham> getSanphamhot(int count)
        {
            DataShopDataContext data = new DataShopDataContext();
            return data.Sanphams.Take(count).ToList();
        }
    }
}