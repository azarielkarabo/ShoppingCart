using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedTime { get; private set; }
        public DateTime? LastUpdatedTimestamp { get; private set; }
        public void SetId()
        {
            Id = Guid.NewGuid();
            CreatedTime = DateTime.Now;
        }
        public void SetUpdatedTimeStamp()
        {
            LastUpdatedTimestamp = DateTime.Now;
        }
    }
}