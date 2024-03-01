using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.Models
{
    public class DesignCart
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public double ProductPrice { get; set; }
        public string ProductImg { get; set; }

        public double QTY {  get; set; }

        public double Total { get; set; }

    }
}