using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Infrastructure;
using MyWebRestaurantApplication.Models.Menu;
using MyWebRestaurantApplication.Models.User;
using System.Linq;

namespace MyWebRestaurantApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;

        public UserController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [Authorize]
        public IActionResult MyProducts()
        {
            
            string userId = this.User.GetId();
            var user = db.Users
                .Include(x => x.ShoppingCart)
                .ThenInclude(x =>x.Meals)
                .ThenInclude(x => x.Ingredients)
                .Where(x => x.Id == userId)
                .FirstOrDefault();
            
            if (user == null)
            {
                return BadRequest();
            }

            var products = user.ShoppingCart.Meals.Select(x => new UserMealsViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                PictureUrl = x.PictureUrl,
                TotalGram = x.TotalGram,
                Count = x.Count,
                Ingredients = x.Ingredients.Select(i => new UserIngredientViewModel
                {
                    Id = i.Id,
                    Name = i.Name

                }).ToList()

            }).ToList();
           
            return View(products);
        }

        [Authorize]
        public IActionResult AddProduct(int Id)
        {
            string userId = this.User.GetId();

            var user = db.Users
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.Meals)
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            if (user == null)
            {
                return BadRequest();
            }

            var meal = db.Meals.Where(x => x.Id == Id).FirstOrDefault();

            if (meal == null)
            {
                return BadRequest();
            }

            if (user.ShoppingCart.Meals.Contains(meal))
            {
                //user.ShoppingCart.Meals.Count++
            }
            user.ShoppingCart.Meals.Add(meal);
            db.SaveChanges();

            return RedirectToAction("MyProducts", "User");
        }

        [Authorize]
        public IActionResult RemoveProduct(int Id)
        {
            string userId = this.User.GetId();
            var user = db.Users
           .Include(x => x.ShoppingCart)
           .ThenInclude(x => x.Meals)
           .Where(x => x.Id == userId)
           .FirstOrDefault();

            var meal = db.Meals.Where(x => x.Id == Id).FirstOrDefault();

            if (meal == null)
            {
                return BadRequest();
            }
            
            user.ShoppingCart.Meals.Remove(meal);
            db.SaveChanges();

            return RedirectToAction("MyProducts", "User");
        }
    }
}
