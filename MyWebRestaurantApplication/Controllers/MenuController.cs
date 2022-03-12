using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Models.Menu;
using System.Linq;

namespace MyWebRestaurantApplication.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext db;

        public MenuController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult CategoryMeals(int Id)
        {

            var categorie = db.Categories.Where(x => x.Id == Id).FirstOrDefault();
            if (categorie == null)
            {
                return BadRequest();
            }

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

            return View(meals);                 
        }
    }
}
