using System;
using System.Collections.Generic;

namespace ShoppingCart.Models
{
    public class Customer : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Suburb { get; set; }
        public string Postcode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public DateTime? ExpDate { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}