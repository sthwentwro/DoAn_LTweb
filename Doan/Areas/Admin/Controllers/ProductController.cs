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
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult ListProduct(int? page)
        {
            int pagesize = 5;
            int pageNum = (page ?? 1);
            ListSP sp = new ListSP();
            return View(sp.listSanpham().ToPagedList(pageNum,pagesize));
        }
        public ActionResult EditProduct(int id)
        {
            ListSP sp = new ListSP();
            return View(model: sp.get1Sanpham(id));
        }
        [HttpPost]
        public ActionResult EditProduct(Sanpham pro)
        {
            ListSP sp = new ListSP();
            var result = sp.EditSP(pro);
            if (result)
            {
                return RedirectToAction("ListProduct");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật sản phẩm không thành công");
            }
            return View("EditProduct");
        }
        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Sanpham pro)
        {
            ListSP sp = new ListSP();
            var result = sp.CreateSP(pro);
            if (result)
            {
                return RedirectToAction("ListProduct");
            }
            else
            {
                ModelState.AddModelError("", "Thêm sản phẩm không thành công");
            }
            return View("CreateProduct");
        }
        public ActionResult DeleteProduct(int id)
        {
            ListSP sp = new ListSP();
            var result = sp.DeleteSP(id);
            if (result)
            {
                return RedirectToAction("ListProduct");
            }
            else
            {
                ModelState.AddModelError("", "Xoá sản phẩm không thành công");
            }
            return RedirectToAction("ListProduct");
        }
    }
}