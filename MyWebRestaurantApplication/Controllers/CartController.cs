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

namespace MyWebRestaurantApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext db;

        public CartController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [Authorize]
        public IActionResult Total()
        {
            var userId = this.User.GetId();
            var user = db.Users
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.Meals)
                .ThenInclude(x => x.Ingredients)
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var products = user.ShoppingCart.Meals.Select(x => new UserMealsViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                PictureUrl = x.PictureUrl,
                Count = x.Count,
                TotalGram = x.TotalGram,
                Ingredients = x.Ingredients.Select(i => new UserIngredientViewModel
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToList(),

            }).ToList();


            var shoppingCart = db.ShoppingCart
                .Where(x => x.UserId == userId)
                .Select(x => new ShoppingCartViewModel
                {
                    Id = x.Id,
                    Meals = products
                }).FirstOrDefault();

            return View(shoppingCart);

        }

        [Authorize]
        public IActionResult ConfirmOrder()
        {
            var userId = this.User.GetId();

            if (userId == null)
            {
                return BadRequest();
            }

            var order = new Order
            {
                UserId = userId,
                DateTime = DateTime.Now
            };

            db.Orders.Add(order);
                 
            var user = db.Users
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.Meals)
                .Where(x => x.Id == userId)
                .FirstOrDefault();
            user.ShoppingCart.Meals.Clear();

            db.SaveChanges();

            return View();
        }
    }
}
