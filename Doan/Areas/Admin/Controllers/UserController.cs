using Doan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Doan.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult ListUser(int? page)
        {
            int pagesize = 10;
            int pageNum = (page ?? 1);
            QLuser listUser = new QLuser();
            return View(listUser.dsUser().ToPagedList(pageNum, pagesize));
        }
        public ActionResult EditUser(int id)
        {
            QLuser userEdit = new QLuser();
            return View(userEdit.ViewDetail(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(Nguoidung user)
        {
            QLuser userEdit = new QLuser();
            bool result = userEdit.UpdateUser(user);
            //luu nguoi dung moi vao database
            //neu update thanh cong thi result la true
            if (result)
            {
                return RedirectToAction("ListUser");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật người dùng không thành công");
            }
            return View("EditUser");
        }
    }
}