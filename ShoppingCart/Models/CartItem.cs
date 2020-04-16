using ShoppingCart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class CartItem : BaseModel
    {
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Product Product { get; set; }
    }
}