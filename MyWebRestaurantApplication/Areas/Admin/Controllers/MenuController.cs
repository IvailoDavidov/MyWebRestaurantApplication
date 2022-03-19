using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Areas.Admin.Models;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using System.Linq;

namespace MyWebRestaurantApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public MenuController(ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        [Authorize]
        public IActionResult AddMeal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMeal(MealAddViewModel meal)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var newMeal = new Meal
            {
                Name = meal.Name,
                Price = meal.Price,
                TotalGram = meal.TotalGram,
                PictureUrl = meal.PictureUrl,
                CategoryId = meal.CategoryId                                 
            };


            if (!db.Meals.Any(x => x.Name == newMeal.Name))
            {
                db.Meals.Add(newMeal);
                db.SaveChanges();
            }


            return RedirectToAction("Gallery", "Home");
        }


    }
}
