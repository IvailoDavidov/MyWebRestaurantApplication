using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Services.Menu;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Controllers
{
    public class MenuController : Controller
    {
       
        private readonly IMenuService menuService;
       

        public MenuController(IMenuService menuService)
        {          
            this.menuService = menuService;          
        }

        public async Task<IActionResult> CategoryMeals(int Id)
        {
            var category = await menuService.CategoryId(Id);

            if (category == null)
            {
                return BadRequest();
            }

            var meals = await menuService.MealsByCategory(Id);          
            return View(meals);                 
        }

        public async Task<IActionResult> Meals()
        {
            var categories = await menuService.Categories();
            return View(categories);
        }
        
        public async Task<IActionResult> Details(int Id)
        {
            var meal = await menuService.Details(Id);
            return View(meal);
        }
    }
}
