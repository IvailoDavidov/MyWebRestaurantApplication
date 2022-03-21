using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebRestaurantApplication.Data.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Meals = new List<Meal>();
        }
      
        [Required]      
        [StringLength(100)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
     
        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Meal> Meals { get; set; }
     
    }
}
