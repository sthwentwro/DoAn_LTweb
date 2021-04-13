using Doan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan.Controllers
{
    public class GiohangController : Controller
    {
        DataShopDataContext data = new DataShopDataContext();
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstgiohang = Session["Giohang"] as List<Giohang>;
            if (lstgiohang == null)
            {
                //neu chua ton tai tao moi lstgiohang
                lstgiohang = new List<Giohang>();
                Session["Giohang"] = lstgiohang;
            }
            return lstgiohang;
        }
        //them gio hang
        public ActionResult Themgiohang(int iId,int sl,string strURL)
        {
            //lay ra session gio hang
            List<Giohang> lstgiohang = Laygiohang();
            //kt sach ton tai trong session chua
            Giohang sp = lstgiohang.Find(n => n.iIDsanpham == iId);
            if (sp == null)
            {
                sp = new Giohang(iId,sl);
                lstgiohang.Add(sp);
                return Redirect(strURL);
            }
            else
            {
                sp.iSoluong++;
                return Redirect(strURL);
            }
        }
        //Tong so luong
        private int Tongsoluong()
        {
            int iTongsoluong = 0;
            List<Giohang> lstgiohang = Session["Giohang"] as List<Giohang>;
            if (lstgiohang != null)
            {
                iTongsoluong = lstgiohang.Sum(n => n.iSoluong);
            }
            return iTongsoluong;
        }
        //tong tien
        private decimal? Tongtien()
        {
            decimal? iTongtien = 0;
            List<Giohang> lstgiohang = Session["Giohang"] as List<Giohang>;
            if (lstgiohang != null)
            {
                iTongtien = lstgiohang.Sum(n => n.dThanhtien);
            }
            return iTongtien;
        }
        //Xay dung trang gio hang
        public ActionResult Giohang()
        {
            List<Giohang> lstgiohang = Laygiohang();
            if (lstgiohang.Count == 0)
            {
                return RedirectToAction("Index", "Books");
            }
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstgiohang);
        }
    }
}