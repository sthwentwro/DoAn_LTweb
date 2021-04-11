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
            return View();
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