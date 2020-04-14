using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class CartItem : BaseModel
    {
        public int Quantity { get; set; }
        public double Total { get; set; }
        public virtual Product Product { get; set; }

    }
}