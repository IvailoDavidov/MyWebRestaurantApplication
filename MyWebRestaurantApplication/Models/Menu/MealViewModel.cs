using System.Collections.Generic;

namespace MyWebRestaurantApplication.Models.Menu
{
    public class MealViewModel
    {
        public int Id { get; set; }
     
        public string Name { get; set; }

        public decimal Price { get; set; }

        public float TotalGram { get; set; }
       
        public string PictureUrl { get; set; }

        public ICollection<IngredientViewModel> Ingredients { get; set; }

    }
}
