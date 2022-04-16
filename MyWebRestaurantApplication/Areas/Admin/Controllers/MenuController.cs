using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Areas.Admin.Models.Menu;
using MyWebRestaurantApplication.Areas.Admin.Services.Menu;
using MyWebRestaurantApplication.Data.Models;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Areas.Admin.Controllers
{
    [Area("Admin")]   
    public class MenuController : Controller
    {      
        private readonly IAdminMenuService adminMenuService;

        public MenuController(IAdminMenuService adminMenuService)
        {          
            this.adminMenuService = adminMenuService;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddMeal()
        {
            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var categories = await adminMenuService.Categories();

            return View(new MealAddEditViewModel
            {
                Categories = categories
            });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddMeal(MealAddEditViewModel meal)
        {

            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            if (adminMenuService.CheckMealExists(meal.Name))
            {
                this.ModelState.AddModelError(nameof(meal.Name), "Meal already exist.");
            }

            if (!adminMenuService.CategoryExists(meal.CategoryId))
            {
                this.ModelState.AddModelError(nameof(meal.CategoryId), "Category does not exist.");
            }


            if (!ModelState.IsValid)
            {
                var categories = await adminMenuService.Categories();
                meal.Categories = categories;

                return View(meal);
            }
           

            var newMeal = new Meal
            {
                Id = meal.Id,
                Name = meal.Name,
                Price = meal.Price,
                TotalGram = meal.TotalGram,
                PictureUrl = meal.PictureUrl,
                CategoryId = meal.CategoryId
            };
           
               await adminMenuService.AddMeal(newMeal);
                       
            return RedirectToAction("Meals", "Menu", new { area = "" });
        }


        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditMeal(int Id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var categories = await  adminMenuService.Categories();  
            var meal = await adminMenuService.GetMealWithCategories(Id, categories);

            return View(meal);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditMeal(int Id, MealAddEditViewModel model)
        {

            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var meal = await adminMenuService.GetMealById(Id);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (meal == null)
            {
                return BadRequest();
            }

            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            await  adminMenuService.EditMeal(meal, model);
            return RedirectToAction("Meals", "Menu", new { area = "" });
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteMeal(int Id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var meal = await adminMenuService.GetMealById(Id);

            if (meal == null)
            {
                return RedirectToAction("Error", "Home");
            }

            await adminMenuService.RemoveMeal(meal);
            return RedirectToAction("Meals", "Menu", new { area = "" });
        }
    }
}
