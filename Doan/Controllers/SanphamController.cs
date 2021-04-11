using Doan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan.Controllers
{
    public class SanphamController : Controller
    {
        // GET: Sanpham
        //Hiện danh sách sản phẩm theo loại
        //Chưa xong
        public ActionResult ListSP()
        {
            ListSP sp = new ListSP();
            return View(sp.getSanpham(null));
        }
        //Hiện chi tiết sản phẩm
        public ActionResult SPdetail(int id)
        {
            ListSP sp = new ListSP();
            return View(model: sp.getSanpham(id).FirstOrDefault());
        }
    }
}