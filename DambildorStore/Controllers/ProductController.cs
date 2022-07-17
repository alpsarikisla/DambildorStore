using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DambildorStore.Models;

namespace DambildorStore.Controllers
{
    public class ProductController : Controller
    {
        DambildorModel db = new DambildorModel();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","Home");
            }
            Product p = db.Products.Find(id);
            return View(p);
        }
    }
}