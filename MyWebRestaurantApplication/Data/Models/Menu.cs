using System;
using System.Collections;
using System.Collections.Generic;

namespace MyWebRestaurantApplication.Data.Models
{
    public class Menu
    {
        public Menu()
        {
            Categories = new HashSet<CategoryMeal>();
        }
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ICollection<CategoryMeal> Categories { get; set; }
       
    }
}
