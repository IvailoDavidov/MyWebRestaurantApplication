using Microsoft.EntityFrameworkCore;
using MyWebRestaurantApplication.Areas.Admin.Models.Menu;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Areas.Admin.Services.Menu
{
    public class AdminMenuService : IAdminMenuService
    {
        private readonly ApplicationDbContext db;

        public AdminMenuService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<ICollection<CategoriesViewModel>> Categories()
        {
            var categories =  await db.Categories
             .Select(x => new CategoriesViewModel
             {
                 Id = x.Id,
                 Name = x.Name
             }).ToListAsync();

            return categories;
        }

        public bool CheckMealExists(string mealName)
        {
            if (db.Meals.Any(x => x.Name == mealName))
            {
                return true;
            }
            return false;
        }

        public async Task<Meal> GetMealById(int mealId)
        {
            var meal = await  db.Meals
               .Where(x => x.Id == mealId)         
               .FirstOrDefaultAsync();

            return meal;
        }

        public async Task AddMeal(Meal meal)
        {
            await db.Meals.AddAsync(meal);
            await db.SaveChangesAsync();
        }

        public async Task RemoveMeal(Meal meal)
        {
             db.Meals.Remove(meal);
             await db.SaveChangesAsync();
        }

        public async Task EditMeal(Meal meal, MealAddEditViewModel model)
        {
            
            meal.Name = model.Name;
            meal.Price = model.Price;
            meal.PictureUrl = model.PictureUrl;
            meal.TotalGram = model.TotalGram;
            meal.CategoryId = model.CategoryId;

            await db.SaveChangesAsync();
        }

        public async Task <MealAddEditViewModel> GetMealWithCategories(int mealId, ICollection<CategoriesViewModel> categories)
        {
            var meal = await db.Meals
                .Where(x => x.Id == mealId)
                .Select(x => new MealAddEditViewModel
                {                   
                    Name = x.Name,
                    Price = x.Price,
                    PictureUrl = x.PictureUrl,
                    TotalGram = x.TotalGram,
                    CategoryId = x.CategoryId,
                    Categories = categories,

                }).FirstOrDefaultAsync();

            return meal;
        }

        public bool CategoryExists(int categoryId)
           => this.db
               .Categories
               .Any(c => c.Id == categoryId);
    }
}
