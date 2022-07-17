using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DambildorStore.Models;

namespace DambildorStore.Controllers
{
    public class HomeController : Controller
    {
        DambildorModel db = new DambildorModel();
        // GET: Home
        public ActionResult Index()
        {
            List<Product> ProductList = db.Products.Where(s=> s.SellStatus == true).ToList();
            return View(ProductList);
        }
    }
}