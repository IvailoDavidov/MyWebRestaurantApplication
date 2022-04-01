using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Infrastructure;
using MyWebRestaurantApplication.Models.Cart;
using MyWebRestaurantApplication.Models.Menu;
using MyWebRestaurantApplication.Models.User;
using System.Linq;
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

            cartService.CreateOrder(order, userId);

            var user = userService.GetById(userId);
            if (user == null)
            {
                return BadRequest();
            }

            cartService.Clear(user);

            return View();
        }
    }
}
