using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Infrastructure;
using MyWebRestaurantApplication.Models.Cart;
using MyWebRestaurantApplication.Services.User;
using MyWebRestaurantApplication.Services.Cart;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Total()
        {
            var userId = this.User.GetId();
            if (userId == null)
            {
                return BadRequest();
            }
           
            var user = await userService.GetById(userId);

            if (user == null)
            {
                return BadRequest();
            }
               
            var shoppingCart = await userService.GetShoppingCart(userId);
            return View(shoppingCart);
        }

        [Authorize]
        public async Task<IActionResult> ConfirmOrder(OrderViewModel order)
        {
            var userId = this.User.GetId();

            if (userId == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userService.GetById(userId);

            if (user == null)
            {
                return BadRequest();
            }

            await cartService.CreateOrder(order, user);

            return View();
        }
    }
}
