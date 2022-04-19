using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Services.Menu
{
    public interface IMenuService
    {
        public Task<ICollection<MealViewModel>> MealsByCategory(int Id);

        public Task<CategoryMeal> CategoryId(int Id);

        public Task<IEnumerable<CategoryViewModel>> Categories();

        public Task<MealViewModel> Details(int Id);
    }
}
