using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Order : BaseModel
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        public string Country { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        public string Email { get; set; }

        public decimal Total { get; set; }

        public string PaymentTransactionId { get; set; }

        [ScaffoldColumn(false)]
        public bool HasBeenShipped { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}