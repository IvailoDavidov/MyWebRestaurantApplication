using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebRestaurantApplication.Data.Models
{
    public class Menu
    {
        public Menu()
        {
            Categories = new HashSet<CategoryMeal>();
        }

        [Required]    
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ICollection<CategoryMeal> Categories { get; set; }
    
    }
}
