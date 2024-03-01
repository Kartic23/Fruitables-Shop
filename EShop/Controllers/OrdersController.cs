using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EShop.Models;
using Microsoft.AspNet.Identity;

namespace EShop.Controllers
{
    public class OrdersController : Controller
    {
        private ShopEntities1 db = new ShopEntities1();

        private CartController cart = new CartController();

        private Orders_itemsController ordersItems = new Orders_itemsController();


        // GET: Orders
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<MyOrders> myOrders = new List<MyOrders>();
                foreach (var item in db.Orders)
                {
                    if(item.UserMail.Equals(User.Identity.Name))
                    {
                        MyOrders order = new MyOrders();
                        order.Order = item;
                        order.Items = new List<Orders_items>();
                        foreach (var orderItem in db.Orders_items)
                        {
                            if(item.Id == orderItem.OrderId)
                            {
                                order.Items.Add(orderItem);
                            }
                        }
                        myOrders.Add(order);
                    }
                }
                return View(myOrders);
            }
            else
            {
                return RedirectToAction("Error", "Products");
            }
        }

        //To make a Order
        public ActionResult MakeOrder()
        {

            if (User.Identity.IsAuthenticated && this.cart.Carts().Count > 0 )
            {
                Order order = new Order();
                order.Id = db.Orders.Count();
                order.orderDay = DateTime.Now;
                order.UserMail = User.Identity.Name;
                order.Total = 0;
                Create(order);
                return RedirectToAction("Index", "Orders");
            }
            else
            {
                return RedirectToAction("Error", "Products");
            }

        }


      

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserMail,Total,orderDay")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                List<Cart> cart = this.cart.Carts();
                double total = 0;
                foreach (var item in cart)
                {
                    Orders_items order_item = new Orders_items();
                    order_item.Qty = item.Qty;
                    order_item.ProductId = item.ProductID;
                    order_item.OrderId = order.Id;
                    ordersItems.Create(order_item);
                    total =  item.Qty * (double) db.Products.Find(item.ProductID).Price;
                    System.Diagnostics.Debug.WriteLine(total);
                }
                total += 3;
                System.Diagnostics.Debug.WriteLine(total);
                order.Total =(decimal) total;
                System.Diagnostics.Debug.WriteLine(order.Total);
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Orders");
            }

            return View(order);
        }

     
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
