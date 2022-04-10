using System.Collections.Generic;
using MyWebRestaurantApplication.Areas.Admin.Models.Menu;
using MyWebRestaurantApplication.Data.Models;

namespace MyWebRestaurantApplication.Areas.Admin.Services.Menu
{
    public interface IAdminMenuService
    {
        public ICollection<CategoriesViewModel> Categories();

        public Meal GetMealById(int mealId);

        public bool CheckMealExists(string mealName);

        public void AddMeal(Meal meal);

        public void RemoveMeal(Meal meal);

        public void EditMeal(Meal meal, MealAddEditViewModel model);

        public MealAddEditViewModel GetMealWithCategories(int mealId, ICollection<CategoriesViewModel> categories);

    }
}
