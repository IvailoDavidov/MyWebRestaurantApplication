using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Infrastructure;
using MyWebRestaurantApplication.Services.User;
using System.Threading.Tasks;

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
        public async Task<IActionResult> MyProducts()
        {
            string userId = this.User.GetId();
            var user = await userService.GetById(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var products = await userService.Products(userId);
            return View(products);
        }


        [Authorize]
        public async Task<IActionResult> AddProduct(int Id)
        {
            string userId = this.User.GetId();
            var user = await userService.GetById(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var meal = await userService.GetMealById(Id);

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

            await userService.SaveProduct(user,meal);
            return RedirectToAction("MyProducts", "User");
        }

        [Authorize]
        public async Task<IActionResult> RemoveProduct(int Id)
        {
            string userId = this.User.GetId();             
            var user = await userService.GetById(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var meal = await userService.GetMealById(Id);

            if (meal == null)
            {
                return BadRequest();
            }

            await userService.RemoveProduct(user, meal);
            return RedirectToAction("MyProducts", "User");
        }       
    }
}
