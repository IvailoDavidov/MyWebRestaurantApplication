using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebRestaurantApplication.Areas.Admin.Models.Menu;
using MyWebRestaurantApplication.Data.Models;

namespace MyWebRestaurantApplication.Areas.Admin.Services.Menu
{
    public interface IAdminMenuService
    {
        public Task<ICollection<CategoriesViewModel>> Categories();

        public Task<Meal> GetMealById(int mealId);

        public bool CheckMealExists(string mealName);

        public Task AddMeal(Meal meal);

        public Task RemoveMeal(Meal meal);

        public Task EditMeal(Meal meal, MealAddEditViewModel model);

        public Task<MealAddEditViewModel> GetMealWithCategories(int mealId, ICollection<CategoriesViewModel> categories);

        public bool CategoryExists(int categoryId);

    }
}
