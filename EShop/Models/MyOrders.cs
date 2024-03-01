using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.Models
{
    public class MyOrders
    {
        public Order Order { get; set; }

        public List<Orders_items> Items { get; set; }
    }
}