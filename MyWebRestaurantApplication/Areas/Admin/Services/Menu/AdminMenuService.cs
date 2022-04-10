using MyWebRestaurantApplication.Areas.Admin.Models.Menu;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWebRestaurantApplication.Areas.Admin.Services.Menu
{
    public class AdminMenuService : IAdminMenuService
    {
        private readonly ApplicationDbContext db;

        public AdminMenuService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public ICollection<CategoriesViewModel> Categories()
        {
            var categories = db.Categories
             .Select(x => new CategoriesViewModel
             {
                 Id = x.Id,
                 Name = x.Name
             }).ToList();

            return categories;
        }

        public bool CheckMealExists(string mealName)
        {
            if (!db.Meals.Any(x => x.Name == mealName))
            {
                return true;
            }
            return false;
        }

        public Meal GetMealById(int mealId)
        {
            var meal = db.Meals
               .Where(x => x.Id == mealId)         
               .FirstOrDefault();

            return meal;
        }

        public void AddMeal(Meal meal)
        {
            db.Meals.Add(meal);
            db.SaveChanges();
        }

        public void RemoveMeal(Meal meal)
        {
            db.Meals.Remove(meal);
            db.SaveChanges();
        }

        public void EditMeal(Meal meal, MealEditViewModel model)
        {
            
            meal.Name = model.Name;
            meal.Price = model.Price;
            meal.PictureUrl = model.PictureUrl;
            meal.TotalGram = model.TotalGram;
            meal.CategoryId = model.CategoryId;

            db.SaveChanges();
        }

        public MealEditViewModel GetMealWithCategories(int mealId, ICollection<CategoriesViewModel> categories)
        {
            var meal = db.Meals
                .Where(x => x.Id == mealId)
                .Select(x => new MealEditViewModel
                {                   
                    Name = x.Name,
                    Price = x.Price,
                    PictureUrl = x.PictureUrl,
                    TotalGram = x.TotalGram,
                    CategoryId = x.CategoryId,
                    Categories = categories,

                }).FirstOrDefault();

            return meal;
        }
    }
}
