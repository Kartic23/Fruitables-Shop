using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductID { get; set; }  
        public int Qty {  get; set; }   

    }
}