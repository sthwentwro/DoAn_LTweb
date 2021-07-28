using Doan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Doan.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sess = (Nguoidung)Session["Taikhoan"];
            if(sess != null)
            {
                if (sess.RoleID != 1)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "LoginAdmin", action = "LoginAdmin", Area = "Admin" }));
                }
            }
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "LoginAdmin", action = "LoginAdmin", Area = "Admin" }));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}