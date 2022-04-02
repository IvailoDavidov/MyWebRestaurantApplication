using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Infrastructure;
using MyWebRestaurantApplication.Models.Cart;
using MyWebRestaurantApplication.Services.User;
using MyWebRestaurantApplication.Services.Cart;

namespace MyWebRestaurantApplication.Controllers
{
    public class CartController : Controller
    {
       
        private readonly IUserService userService;
        private readonly ICartService cartService;

        public CartController(IUserService userService , ICartService cartService)
        {
            
            this.userService = userService;
            this.cartService = cartService;
        }

        [Authorize]
        public IActionResult Total()
        {
            var userId = this.User.GetId();
            if (userId == null)
            {
                return BadRequest();
            }
           
            var user = userService.GetById(userId);

            if (user == null)
            {
                return BadRequest();
            }
               
            var shoppingCart = userService.GetShoppingCart(userId);
            return View(shoppingCart);
        }

        [Authorize]
        public IActionResult ConfirmOrder(OrderViewModel order)
        {
            var userId = this.User.GetId();

            if (userId == null)
            {
                return BadRequest();
            }

            var user = userService.GetById(userId);

            if (user == null)
            {
                return BadRequest();
            }

            cartService.CreateOrder(order, user);
            cartService.Clear(user);

            return View();
        }
    }
}
