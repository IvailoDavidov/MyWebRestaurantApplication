using System;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Cart;

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

        public void CreateOrder(OrderViewModel order, Data.Models.User user)
        {
           
            var newOrder = new Order
            {              
               UserId = user.Id,
               UserAdress = user.Address,              
               DateTime = DateTime.Now              
            };

            db.Orders.Add(newOrder);
            db.SaveChanges();
        }
    }
}
