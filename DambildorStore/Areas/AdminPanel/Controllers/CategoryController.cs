using DambildorStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DambildorStore.Areas.AdminPanel.Controllers
{
    public class CategoryController : Controller
    {
        DambildorModel db = new DambildorModel();
        // GET: AdminPanel/Category
        public ActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditCategory(int? id)//Nullable integer
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Category category = db.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Category c = db.Categories.Find(id);
            c.Status = false;//Soft Delete
            //db.Categories.Remove(c);//Hard Delete
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}