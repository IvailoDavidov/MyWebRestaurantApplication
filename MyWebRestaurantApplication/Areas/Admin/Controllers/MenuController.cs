using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Areas.Admin.Models.Menu;
using MyWebRestaurantApplication.Areas.Admin.Services.Menu;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;

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
        public IActionResult AddMeal()
        {
            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var categories = adminMenuService.Categories();

            return View(new MealAddViewModel
            {
                Categories = categories
            });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddMeal(MealAddViewModel meal)
        {

            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                var categories = adminMenuService.Categories();
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


            if (adminMenuService.CheckMealExists(newMeal.Name))
            {
                adminMenuService.AddMeal(newMeal);
            }

            return RedirectToAction("Meals", "Menu", new { area = "" });
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult EditMeal(int Id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var categories = adminMenuService.Categories();  
            var meal = adminMenuService.GetMealWithCategories(Id, categories);

            return View(meal);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditMeal(int Id, MealEditViewModel model)
        {

            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var meal = adminMenuService.GetMealById(Id);

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

            adminMenuService.EditMeal(meal, model);
            return RedirectToAction("Meals", "Menu", new { area = "" });
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteMeal(int Id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var meal = adminMenuService.GetMealById(Id);

            if (meal == null)
            {
                return RedirectToAction("Error", "Home");
            }

            adminMenuService.RemoveMeal(meal);
            return RedirectToAction("Meals", "Menu", new { area = "" });
        }
    }
}
