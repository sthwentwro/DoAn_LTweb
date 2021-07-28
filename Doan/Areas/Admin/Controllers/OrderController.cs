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
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult ListOrder(int? page)
        {
            int pagesize = 6;
            int pageNum = (page ?? 1);
            QLdondathang ddh = new QLdondathang();
            return View(ddh.dsDDH().ToPagedList(pageNum, pagesize));
        }

        [HttpGet]
        public ActionResult EditOrder(int id)
        {
            QLdondathang ddh = new QLdondathang();
            return View(ddh.CTHD(id));
        }
        [HttpPost]
        public ActionResult EditOrder(HoaDon don)
        {
            QLdondathang ddh = new QLdondathang();
            bool result = ddh.UpdateHD(don);
            if (result)
            {
                return RedirectToAction("ListOrder");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật đơn hàng không thành công");
            }
            return View("EditOrder");
        }
        public ActionResult RemoveOrder(int id)
        {
            QLdondathang ddh = new QLdondathang();
            bool result = ddh.RemoveHD(id);
            if (result)
            {
                return RedirectToAction("ListOrder");
            }
            else
            {
                ModelState.AddModelError("", "Xóa đơn hàng không thành công");
            }
            return View("ListOrder");
        }
    }
}