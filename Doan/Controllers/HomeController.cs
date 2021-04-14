using Doan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ListSP sp = new ListSP();
            var listSP = sp.listSanpham();
            //lấy 4 sp ngẫu nhiên trong danh sách
            List<Sanpham> list4sp = new List<Sanpham>(); 
            Random ran = new Random();
            while(list4sp.Count<4)
            {
                int r = ran.Next(listSP.Count - 1);
                //kiểm tra sp đó có trong danh sách chưa
                if (!list4sp.Contains(listSP[r]))
                {
                    list4sp.Add(listSP[r]);
                }
            }
            return View(list4sp);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Trang About";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Trang lien he";

            return View();
        }
    }
}