using System.Collections.Generic;

namespace MyWebRestaurantApplication.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Gram { get; set; }

        public ICollection<Meal> Meals { get; set; }

    }
}
