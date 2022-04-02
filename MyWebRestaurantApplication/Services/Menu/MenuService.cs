using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Menu;
using System.Collections.Generic;
using System.Linq;

namespace MyWebRestaurantApplication.Services.Menu
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext db;

        public MenuService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public CategoryMeal CategoryId(int Id)
        {
            var categorie = db.Categories.Where(x => x.Id == Id).FirstOrDefault();
            return  categorie;
        }

        public IEnumerable<CategoryViewModel> Categories()
        {
            var categories = db.Categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                PictureUrl = x.PictureUrl

            }).ToList();

            return categories;
        }

        public ICollection<MealViewModel> MealsByCategory(int Id)
        {

            var meals = db.Meals.Where(x => x.CategoryId == Id).Select(x => new MealViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                PictureUrl = x.PictureUrl,
                Ingredients = x.Ingredients.Select(i => new IngredientViewModel
                {
                    Name = i.Name,

                }).ToList(),

            }).ToList();

            return meals;
        }

        public MealViewModel Details(int Id)
        {
            var meal = db.Meals
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
               }).FirstOrDefault();

            return meal;
        }
    }
}
