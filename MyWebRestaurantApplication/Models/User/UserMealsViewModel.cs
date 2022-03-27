using MyWebRestaurantApplication.Models.Menu;
using System.Collections.Generic;

namespace MyWebRestaurantApplication.Models.User
{
    public class UserMealsViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public float TotalGram { get; set; }

        public string PictureUrl { get; set; }

        public int Count { get; set; }

        public ICollection<UserIngredientViewModel> Ingredients { get; set; }
    }
}
