using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Models.Menu;
using MyWebRestaurantApplication.Services.Menu;
using System.Linq;

namespace MyWebRestaurantApplication.Controllers
{
    public class MenuController : Controller
    {
       
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
           
            this.menuService = menuService;
        }
        public IActionResult CategoryMeals(int Id)
        {

            var category = menuService.CategoryId(Id);

            if (category == null)
            {
                return BadRequest();
            }

            var meals = menuService.MealsByCategory(Id);
           
            return View(meals);                 
        }

        public IActionResult Meals()
        {
            var categories = menuService.Categories();
            return View(categories);
        }

        public IActionResult Events()
        {
            return View();
        }
    }
}
