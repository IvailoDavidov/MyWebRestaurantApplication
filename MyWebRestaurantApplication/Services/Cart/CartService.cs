using System;
using Microsoft.AspNetCore.Identity;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Cart;
using MyWebRestaurantApplication.Services.User;
using System.Security.Claims;

namespace MyWebRestaurantApplication.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext db;
   

        public CartService(ApplicationDbContext db)
        {
            this.db = db;
         
        }

        public void Clear(Data.Models.User user)
        {
            user.ShoppingCart.Meals.Clear();
            db.SaveChanges();
        }

        public void CreateOrder(OrderViewModel order, string userId)
        {
           
            var newOrder = new Order
            {              
               UserId = userId,
               DateTime = DateTime.Now              
            };

            db.Orders.Add(newOrder);
            db.SaveChanges();
        }
    }
}
