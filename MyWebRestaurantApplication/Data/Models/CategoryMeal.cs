using System;
using System.Collections.Generic;

namespace MyWebRestaurantApplication.Data.Models
{
    public class CategoryMeal
    {
        public CategoryMeal()
        {
            Meals = new HashSet<Meal>();
        }
        public int Id { get; set; } 

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public int NumberOfMeals { get; set; }

        public ICollection<Meal> Meals{ get; set; }

    }
}
