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

    
        public IActionResult AddMeal()
        {
            var categories = db.Categories.Select(x => new CategoriesViewModel 
            { 
                Id = x.Id, 
                Name = x.Name
            }).ToList();

            return View(new MealAddViewModel
            {
                Categories = categories
            });
        }

        [HttpPost]
        public IActionResult AddMeal(MealAddViewModel meal)
        {

            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                var categories = db.Categories.Select(x => new CategoriesViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

                meal.Categories = categories;

                return View(meal);
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


            return RedirectToAction("Gallery", "Home", new { area = "" });
           
        }


        public IActionResult EditMeal(int Id)
        {

            var categories = db.Categories.Select(x => new CategoriesViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();


            var meal = db.Meals.Where(x => x.Id == Id).Select(x => new EditViewModel
            {
                Name = x.Name,
                Price = x.Price,
                PictureUrl = x.PictureUrl,
                TotalGram = x.TotalGram,
                CategoryId = x.CategoryId,
                Categories = categories

            }).FirstOrDefault();


            return View(meal);
          
        }

        [HttpPost]
        public IActionResult EditMeal(int Id, EditViewModel model)
        {

            var categories = db.Categories.Select(x => new CategoriesViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            var meal = db.Meals.Where(x => x.Id == Id).FirstOrDefault();
             

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

            meal.Name = model.Name;
            meal.Price = model.Price;
            meal.PictureUrl = model.PictureUrl;
            meal.TotalGram = model.TotalGram;
            meal.CategoryId = model.CategoryId;
            

            this.db.SaveChanges();
            return RedirectToAction("CategoryMeals","Menu", new { area = "" });
        }

        public IActionResult DeleteMeal(int Id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return Unauthorized();
            }

            var meal = db.Meals.Where(x => x.Id == Id).FirstOrDefault();

            if (meal == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.Meals.Remove(meal);
            db.SaveChanges();

            return RedirectToAction("Gallery", "Home", new { area = "" });
        }
    }

}
