using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Product : BaseModel
    {
        public string ImagePath { get; set; }
        public double? Price { get; set; }
        public virtual Category Category { get; set; }
    }
}