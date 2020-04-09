using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Api.v1.Model
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastUpdatedTimestamp { get; set; }
        public string ImagePath { get; set; }
        public double? Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}