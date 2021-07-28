using ATTT.Common;
using Doan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        DataShopDataContext db = new DataShopDataContext();
        // GET: Admin/LoginAdmin
        public ActionResult LoginAdmin()
        {
            return View();
        }
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["name"];
            var mkdn = collection["pass"];
            if (ModelState.IsValid)
            {
                //gán giá trị đối tượng được tạo mới
                Nguoidung kh = db.Nguoidungs.SingleOrDefault(n => n.Username == tendn && n.Password == MaHoaMD5.MD5Hash(mkdn));
                //neu ket qua dung thi chuyen qua trang home
                //neu ket qua sai thi cho dang nhap lai
                if (kh != null)
                {                  
                    //neu tai khoan la cua admin thi cho vao
                    if (kh.RoleID == 1)
                    {
                        Session["Taikhoan"] = kh;
                        return RedirectToAction("ListOrder", "Order");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không có quyền truy cập trang này");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }
            return View("LoginAdmin");
        }
        public ActionResult Logout()
        {
            Session["Taikhoan"] = null;
            return View("LoginAdmin");
        }
    }
}