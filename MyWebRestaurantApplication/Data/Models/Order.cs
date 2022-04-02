using System;
using System.ComponentModel.DataAnnotations;

namespace MyWebRestaurantApplication.Data.Models
{
    public class Order
    {
        [Required]
        [StringLength(100)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; }

        public User User { get; set; }

  
        [StringLength(100)]
        public string UserAdress { get; set; }

        public DateTime DateTime { get; set; }
    }
}
