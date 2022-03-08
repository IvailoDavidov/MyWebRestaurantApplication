using System.Collections.Generic;

namespace MyWebRestaurantApplication.Data.Models
{
    public class Meal
    {
        public Meal()
        {
            Ingredients = new HashSet<Ingredient>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public float TotalGram { get; set; }

        public int CategoryId { get; set; }

        public CategoryMeal CategoryMeal { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
