using ATTT.Common;
using Doan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan.Controllers
{
    public class NguoidungController : Controller
    {
        // GET: NguoiDung
        DataShopDataContext data = new DataShopDataContext();

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendn = collection["Username"];
            var mkdn = collection["pass"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(mkdn))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //gán giá trị đối tượng được tạo mới
                Nguoidung kh = data.Nguoidungs.SingleOrDefault(n => n.Username == tendn && n.Password == MaHoaMD5.MD5Hash(mkdn));
                if (kh != null)
                {
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }           
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        public ActionResult DangKy(FormCollection collection, Nguoidung kh)
        {
            // gác các giá trị người dùng nhập liệu cho các biến
            var hoten = collection["HoTen"];
            var tendn = collection["Username"];
            var matkhau = collection["pass"];
            var matkhaunhaplai = collection["passconfirm"];
            var diachi = collection["Diachi"];
            var role = collection["Role"];


            var sdt = collection["SDT"];

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["loi"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi3"] = "phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["loi4"] = "Phải nhập lại mật khẩu";
            }
            else if (matkhau != matkhaunhaplai)
            {
                ModelState.AddModelError("","Hai mật khẩu phải trùng nhau");
                return View("DangKy");
            }
            if (String.IsNullOrEmpty(sdt))
            {
                ViewData["loi6"] = "Phải nhập điện thoại";
            }
            if (String.IsNullOrEmpty(diachi))
            {
                ViewData["loi7"] = "Phải nhập địa chỉ";
            }
            else
            {
                kh.HoTen = hoten;
                kh.Username = tendn;
                kh.Password = MaHoaMD5.MD5Hash(matkhau);

                kh.Diachi = diachi;
                kh.SDT = sdt;
                kh.RoleID = 2;

                data.Nguoidungs.InsertOnSubmit(kh);
                data.SubmitChanges();
                Session["Taikhoan"] = kh;
                return RedirectToAction("Index", "Home");
            }
            return this.DangKy();
        }
        public ActionResult Logout()
        {
            Session["Taikhoan"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}