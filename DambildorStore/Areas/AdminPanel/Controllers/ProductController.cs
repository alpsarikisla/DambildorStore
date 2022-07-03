using DambildorStore.Areas.AdminPanel.Filters;
using DambildorStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DambildorStore.Areas.AdminPanel.Controllers
{
    [AdminAuthenticationFilter]
    public class ProductController : Controller
    {
        DambildorModel db = new DambildorModel();
        // GET: AdminPanel/Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.murtaza = "Selam";
            //ViewBag.aysenur = "Sanada Selam";
            ViewBag.Category_ID = new SelectList(db.Categories.Where(x=> x.Status==true), "ID", "Name");
            ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.status == true), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product model, HttpPostedFileBase resim)
        {
            if (ModelState.IsValid)
            {
                if (resim != null)
                {
                    FileInfo fi = new FileInfo(resim.FileName);
                    string imagename = Guid.NewGuid().ToString() + fi.Extension;
                    model.CoverImage = imagename;
                    resim.SaveAs(Server.MapPath("~/Images/ProductImages/"+ imagename));
                }
                else
                {
                    model.CoverImage = "none.png";
                }
                model.CreationDay = DateTime.Now;
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("AddImage");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult AddImage()
        {
            
            return View();
        }
    }
}