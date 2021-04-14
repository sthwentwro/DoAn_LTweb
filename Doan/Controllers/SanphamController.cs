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
        //public ActionResult ListSP()
        //{
        //    ListSP sp = new ListSP();
        //    return View(sp.getSanpham(null));
        //}
        //Hiện chi tiết sản phẩm
        public ActionResult SPdetail(int id)
        {
            ListSP sp = new ListSP();
            return View(model: sp.getSanpham(id).FirstOrDefault());
        }
        //Hiện danh sách sản phẩm theo loại
        public ActionResult ListSPCategory(int loai)
        {
            ListSP sp = new ListSP();
            return View(sp.getSanphamtheoloai(loai));
        }
    }
}