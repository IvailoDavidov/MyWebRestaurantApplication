using Microsoft.EntityFrameworkCore;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Cart;
using MyWebRestaurantApplication.Models.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Data.Models.User> GetById(string Id)
        {
            var user = await db.Users
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.Meals)
                .ThenInclude(x => x.Ingredients)
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<Meal> GetMealById(int mealId)
        {
            var meal = await db.Meals
                .Where(x => x.Id == mealId).FirstOrDefaultAsync();

            return meal;
        }

        public async Task<ShoppingCartViewModel> GetShoppingCart(string userId)
        {
            var shoppingCart = await db.User
               .Where(x => x.Id == userId)
               .Select(u => new ShoppingCartViewModel
               {
                   Id = u.ShoppingCartId,
                   Meals = u.ShoppingCart.Meals.Select(m => new UserMealsViewModel
                   {
                       Id = m.Id,
                       Name = m.Name,
                       PictureUrl = m.PictureUrl,
                       Price = m.Price,
                       Count = m.Count,
                       TotalGram = m.TotalGram,
                       Ingredients = m.Ingredients
                       .Select(i => new UserIngredientViewModel
                       {
                           Id = i.Id,
                           Name = i.Name
                       }).ToList(),
                   }).ToList()
               })
               .FirstOrDefaultAsync();

            return shoppingCart;
        }

        public async Task<IEnumerable<UserMealsViewModel>> Products(string userId)
        {
            var user = await GetById(userId);

            var products = user
                .ShoppingCart
                .Meals
                .Select(x => new UserMealsViewModel
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

            return products;
        }

        public async Task RemoveProduct(Data.Models.User user, Meal meal)
        {
            if (meal.Count > 1)
            {
                meal.Count--;
                await db.SaveChangesAsync();
            }
            else
            {
                user.ShoppingCart.Meals.Remove(meal);
                await db.SaveChangesAsync();
            }
        }

        public async Task SaveProduct(Data.Models.User user, Meal meal)
        {
            user.ShoppingCart.Meals.Add(meal);
            await  db.SaveChangesAsync();
        }
    }
}
