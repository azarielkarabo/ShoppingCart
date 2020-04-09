using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Category : BaseModel
    {
        public virtual ICollection<Product> Products { get; set; }
    }
}