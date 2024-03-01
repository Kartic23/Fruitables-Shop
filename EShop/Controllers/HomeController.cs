using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.Controllers
{
    public class HomeController : Controller
    {
        public CartController carts = new CartController();
        public ActionResult Index()
        {
            ViewBag.size = carts.Carts().Count;
            return View();
        }
    }
}