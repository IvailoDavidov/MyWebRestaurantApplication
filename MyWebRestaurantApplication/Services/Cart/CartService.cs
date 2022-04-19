using System;
using System.Threading.Tasks;
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

        public async Task CreateOrder(OrderViewModel order, Data.Models.User user)
        {           
            var newOrder = new Order
            {              
               UserId = user.Id,
               UserAdress = user.Address,              
               DateTime = DateTime.Now              
            };

            await db.Orders.AddAsync(newOrder);
            user.ShoppingCart.Meals.Clear();
            await db.SaveChangesAsync();
        }
    }
}
