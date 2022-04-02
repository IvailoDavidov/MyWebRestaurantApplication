using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Menu;
using System.Collections.Generic;

namespace MyWebRestaurantApplication.Services.Menu
{
    public interface IMenuService
    {
        public ICollection<MealViewModel> MealsByCategory(int Id);

        public CategoryMeal CategoryId(int Id);

        public IEnumerable<CategoryViewModel> Categories();

        public MealViewModel Details(int Id);
    }
}
