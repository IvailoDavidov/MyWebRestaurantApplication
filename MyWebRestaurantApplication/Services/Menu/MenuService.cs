using Microsoft.EntityFrameworkCore;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Menu;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Services.Menu
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext db;

        public MenuService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<CategoryMeal> CategoryId(int Id)
        {
            var category = await db.Categories
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

            return  category;
        }

        public async Task<IEnumerable<CategoryViewModel>> Categories()
        {
            var categories = await db.Categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                PictureUrl = x.PictureUrl

            }).ToListAsync();

            return categories;
        }

        public async Task<ICollection<MealViewModel>> MealsByCategory(int Id)
        {

            var meals = await db.Meals.Where(x => x.CategoryId == Id).Select(x => new MealViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                PictureUrl = x.PictureUrl,
                Ingredients = x.Ingredients.Select(i => new IngredientViewModel
                {
                    Name = i.Name,

                }).ToList(),

            }).ToListAsync();

            return meals;
        }

        public async Task<MealViewModel> Details(int Id)
        {
            var meal = await db.Meals
                .Where(x => x.Id == Id)
               .Select(x => new MealViewModel
               {
                   Name = x.Name,
                   Price = x.Price,
                   PictureUrl = x.PictureUrl,
                   Count = x.Count,
                   TotalGram = x.TotalGram,
                   Ingredients = x.Ingredients
                   .Select(i => new IngredientViewModel 
                   { 
                       Name = i.Name
                   }).ToList(),
               }).FirstOrDefaultAsync();

            return meal;
        }
    }
}
