using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Api.v1.Model
{
    public class OrderDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastUpdatedTimestamp { get; set; }
        public string Username { get; set; }
        public int Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public virtual Guid ProductId { get; set; }
        public virtual Guid OrderId { get; set; }
    }
}