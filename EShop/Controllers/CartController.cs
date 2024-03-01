using EShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using Antlr.Runtime.Tree;


namespace EShop.Controllers
{
    public class CartController : Controller
    {
        
        private static List<Cart> carts = new List<Cart>();

        private ShopEntities1 ShopEntities = new ShopEntities1();

        public List<Cart> Carts() {    
                return carts;  
        }

        public ActionResult AddItem(int id)
        {
            //
            int find = 0;
            foreach (var item in carts)
            {
                if (item.ProductID == id)
                {
                    item.Qty++;
                    find++;
                    break;
                }
            }
            if (find == 0) {
                Cart cart = new Cart();
                cart.Id = carts.Count;
                cart.ProductID = id;
                cart.Qty = 1;
                carts.Add(cart);
            }
            return RedirectToAction("Index","Products",new { id = 0 });
        }


        public ActionResult DeleteItem(Cart cart)
        {
            carts.Remove(cart);
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult RemoveItem(int id)
        {
            foreach (var item in carts)
            {
                if (item.ProductID == id)
                {
                    return DeleteItem(item);
                }
            }
            ViewBag.size = carts.Count;
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult DincrementItem(int id)
        {
            foreach (var item in carts)
            {
                if (item.ProductID == id)
                {
                    item.Qty--;
                    if(item.Qty <= 0)
                        return DeleteItem(item);
                    break;
                }
            }
            ViewBag.size = carts.Count;
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult IncrementItem(int id)
        {
            foreach (var item in carts)
            {
                if (item.ProductID == id)
                {
                    item.Qty++;
                    break;
                }
            }
            ViewBag.size = carts.Count;
            return RedirectToAction("Index", "Cart");
        }


        // GET: Cart
        public ActionResult Index()
        {
            
            List<DesignCart> list = new List<DesignCart>();
            foreach (var item in carts)
            {
                DesignCart cart = new DesignCart();
                Product product = new Product();
                foreach (var item1 in ShopEntities.Products)
                {
                    if(item1.Id == item.ProductID)
                    {
                        product = item1;
                        break;
                    }
                        
                }
                cart.Id = item.Id;
                cart.ProductID = item.ProductID;
                cart.QTY = item.Qty; 
                cart.ProductPrice = (double) product.Price;
                cart.ProductName = product.Name;
                cart.ProductImg = product.Img; 
                cart.Total = cart.QTY * cart.ProductPrice;
                list.Add(cart);
            }
            ViewBag.size = carts.Count;
            return View(list);
        }
    }
}