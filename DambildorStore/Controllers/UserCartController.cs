using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DambildorStore.Models;

namespace DambildorStore.Controllers
{
    public class UserCartController : Controller
    {
        DambildorModel db = new DambildorModel();
        // GET: UserCart
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                int id = ((User)Session["user"]).ID;
                return View(db.UserCarts.Where(x=> x.User_ID == id).ToList());
                
            }
            return RedirectToAction("Login","User");
        }

        public ActionResult Add(int? id)
        {
            if (id != null)
            {
                if (Session["user"] != null)
                {

                    UserCart uc = new UserCart();
                    uc.User_ID = ((User)Session["user"]).ID;
                    uc.Product_ID = Convert.ToInt32(id);
                    uc.Quantity = 1;
                    uc.CreationDate = DateTime.Now;
                    db.UserCarts.Add(uc);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            return RedirectToAction("Index","Home");
        }


        public ActionResult increase(int id)
        {
            UserCart uc = db.UserCarts.Find(id);
            uc.Quantity += 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult decrease(int id)
        {
            UserCart uc = db.UserCarts.Find(id);
            if (uc.Quantity != 1)
            {
                uc.Quantity -= 1;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Buyproducts()
        {
            if (Session["user"] != null)
            {
                int id = ((User)Session["user"]).ID;
                List<UserCart> userCartList = db.UserCarts.Where(x => x.User_ID == id).ToList();

                ViewBag.semitotal = userCartList.Sum(x => x.Quantity * x.Product.Price) * 0.82m;
                ViewBag.totalTax = userCartList.Sum(x => x.Quantity * x.Product.Price) * 0.18m;
                ViewBag.total = userCartList.Sum(x => x.Quantity * x.Product.Price);
                return View();
            }
            return RedirectToAction("Login", "User");
        }
    }
}