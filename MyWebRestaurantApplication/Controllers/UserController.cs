using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Infrastructure;
using MyWebRestaurantApplication.Models.Cart;
using MyWebRestaurantApplication.Models.Menu;
using MyWebRestaurantApplication.Models.User;
using MyWebRestaurantApplication.Services.User;
using System.Linq;

namespace MyWebRestaurantApplication.Controllers
{
    public class UserController : Controller
    {
        
        private readonly IUserService userService;

        public UserController( IUserService userService)
        {           
            this.userService = userService;
        }

        [Authorize]
        public IActionResult MyProducts()
        {

            string userId = this.User.GetId();
            var user = userService.GetById(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var products = userService.Products(userId);
            return View(products);
        }


        [Authorize]
        public IActionResult AddProduct(int Id)
        {
            string userId = this.User.GetId();

            var user = userService.GetById(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var meal = userService.GetMealById(Id);

            if (meal == null)
            {
                return BadRequest();
            }


            if (user.ShoppingCart.Meals.Contains(meal))
            {
                if (meal.Count == 0)
                {
                    meal.Count = 1;
                    meal.Count++;
                }
                else
                {
                    meal.Count++;
                }

            }
            else
            {
                meal.Count = 1;
            }

            userService.SaveProduct(user,meal);

            return RedirectToAction("MyProducts", "User");
        }

        [Authorize]
        public IActionResult RemoveProduct(int Id)
        {
            string userId = this.User.GetId();           

            var user = userService.GetById(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var meal = userService.GetMealById(Id);

            if (meal == null)
            {
                return BadRequest();
            }

       

            userService.RemoveProduct(user, meal);

            return RedirectToAction("MyProducts", "User");
        }
    }
}
