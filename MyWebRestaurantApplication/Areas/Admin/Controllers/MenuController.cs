using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Areas.Admin.Models;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using System.Threading.Tasks;


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

            return View();
        }

        [HttpPost]
        public IActionResult AddMeal(MealAddViewModel model)
        {
            var meal = new Meal
            {
                Name = model.Name,
                Price = model.Price,
                TotalGram = model.TotalGram,
                PictureUrl = model.PictureUrl,
                CategoryId = model.CategoryId,               
            };

            db.Meals.Add(meal);
            db.SaveChanges();
            return RedirectToAction("Gallery", "Home");
        }
    }
}
