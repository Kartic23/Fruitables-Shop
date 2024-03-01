using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EShop.Models;

namespace EShop.Controllers
{
    public class Orders_itemsController : Controller
    {
        private ShopEntities1 db = new ShopEntities1();

        // GET: Orders_items
        public ActionResult Index()
        {
            var orders_items = db.Orders_items.Include(o => o.Order).Include(o => o.Product);
            return View(orders_items.ToList());
        }

        // GET: Orders_items/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "UserMail");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: Orders_items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,ProductId,Qty")] Orders_items orders_items)
        {
            if (ModelState.IsValid)
            {
                db.Orders_items.Add(orders_items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "Id", "UserMail", orders_items.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", orders_items.ProductId);
            return View(orders_items);
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
