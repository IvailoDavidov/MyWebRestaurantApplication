using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebRestaurantApplication.Data.Models
{
    public class CategoryMeal
    {
        public CategoryMeal()
        {
            Meals = new HashSet<Meal>();
        }
        public int Id { get; set; } 

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        public int? NumberOfMeals { get; set; }

        public ICollection<Meal> Meals{ get; set; }

    }
}
