using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DambildorStore.Models;
using DambildorStore.Models.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;


namespace DambildorStore.Controllers
{
    public class PayServiceModel
    {
        public string MerchantCode { get; set; }
        public string MerchantPassword { get; set; }
        public string CardNo { get; set; }
        public string ExpM { get; set; }
        public string ExpY { get; set; }
        public string CVV { get; set; }
        public decimal Price { get; set; }
    }
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

        [HttpPost]
        public ActionResult Buyproducts(PayViewModel model)
        {
            int id = ((User)Session["user"]).ID;
            List<UserCart> userCartList = db.UserCarts.Where(x => x.User_ID == id).ToList();
            decimal price = userCartList.Sum(x => x.Quantity * x.Product.Price);

            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44375/API/PayService");
                        //client.BaseAddress = new Uri("https://www.hortumbank.com.tr/API/PayService");
                        PayServiceModel requestModel = new PayServiceModel()
                        {
                            MerchantCode = "12XYZ458",
                            MerchantPassword = "14?*WE12",
                            CardNo = model.CardNumber,
                            ExpM = model.ExpM,
                            ExpY = model.ExpY,
                            CVV = model.Cvv,
                            Price = price
                        };
                        //Install-Package Microsoft.AspNet.WebApi.Client
                        var postTask = client.PostAsJsonAsync<PayServiceModel>("PayService", requestModel);
                        postTask.Wait();
                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var stringResp = result.Content.ReadAsStringAsync();
                            if (stringResp.Result == "\"101\"")
                            {
                                return RedirectToAction("PaySuccess", "UserCart");
                            }
                            else if(stringResp.Result == "\"401\"")
                            {
                                ViewBag.semitotal = userCartList.Sum(x => x.Quantity * x.Product.Price) * 0.82m;
                                ViewBag.totalTax = userCartList.Sum(x => x.Quantity * x.Product.Price) * 0.18m;
                                ViewBag.total = userCartList.Sum(x => x.Quantity * x.Product.Price);

                                ViewBag.result = "Bakiye Yetersiz";
                                return View();
                            }
                        }
                    }
                }
                catch
                {

                }
            }
           
            return View(model);
        }
        [HttpGet]
        public ActionResult PaySuccess()
        {
            return View();
        }
    }
}