using Doan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
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
        public ActionResult Themgiohang(int iId,string strURL,FormCollection f)
        {
            //lay ra session gio hang
            List<Giohang> lstgiohang = Laygiohang();
            //kt sach ton tai trong session chua
            Giohang sp = lstgiohang.Find(n => n.iIDsanpham == iId);
            if (sp == null)
            {
                sp = new Giohang(iId,int.Parse(f["soluong"].ToString()));
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
        //hien so luong hang trong gio
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return PartialView();
        }
        //Xay dung trang gio hang
        public ActionResult Giohang(int? page)
        {
            int pagesize = 2;
            int pageNum = (page ?? 1);
            List<Giohang> lstgiohang = Laygiohang();
            if (lstgiohang.Count == 0)
            {
                return View("bag0");
            }
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstgiohang.ToPagedList(pageNum, pagesize));
        }
        [HttpGet]
        //xoa hang trong gio
        public ActionResult Xoagiohang(int? iIDsanpham)
        {
            List<Giohang> lstgiohang = Laygiohang();
            Giohang sp = lstgiohang.SingleOrDefault(n => n.iIDsanpham == iIDsanpham);
            if (sp != null)
            {
                lstgiohang.RemoveAll(n => n.iIDsanpham == iIDsanpham);
                if (lstgiohang.Count > 0)
                {
                    return RedirectToAction("Giohang");
                }
                else
                {
                    return View("bag0");
                }             
            }
            if (lstgiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Giohang");
        }
        //cap nhat lai so luong va thanh tien khi nguoi dung thay doi
        public ActionResult Capnhatgiohang(int? iMaSP, FormCollection f)
        {
            List<Giohang> lstgiohang = Laygiohang();
            Giohang sp = lstgiohang.SingleOrDefault(n => n.iIDsanpham == iMaSP);
            if (sp != null)
            {
                sp.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        //xoa tat ca san pham co trong gio hang
        public ActionResult Xoatatca()
        {
            List<Giohang> lstgiohang = Laygiohang();
            lstgiohang.Clear();
            return View("bag0");
        }
        //phuong thuc dat hang
        //neu chua dang nhap thi khong dc dat hang
        [HttpGet]
        public ActionResult Dathang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Nguoidung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }
        public ActionResult Dathang(FormCollection collection)
        {
            Nguoidung kh = (Nguoidung)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            //kiem tra ngay dat giao phai sau ngay dat
            if (DateTime.Parse(ngaygiao) < DateTime.Now)
            {
                ModelState.AddModelError("", "Ngày giao không hợp lệ");
                return RedirectToAction("Dathang","Giohang");
            }
            //chay phuong thuc dat hang luu vao database
            Giohang giohang = new Giohang();
            giohang.Dathang(kh, gh, ngaygiao);
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}