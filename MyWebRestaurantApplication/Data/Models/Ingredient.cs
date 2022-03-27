using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebRestaurantApplication.Data.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            Meals = new List<Meal>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

      
        public ICollection<Meal> Meals { get; set; }

    }
}
