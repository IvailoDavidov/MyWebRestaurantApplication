using Microsoft.EntityFrameworkCore;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Cart;
using MyWebRestaurantApplication.Models.User;
using System.Collections.Generic;
using System.Linq;

namespace MyWebRestaurantApplication.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Data.Models.User GetById(string Id)
        {
            var user = db.Users
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.Meals)
                .ThenInclude(x => x.Ingredients)
                .Where(x => x.Id == Id)
                .FirstOrDefault();

            return user;
        }

        public Meal GetMealById(int mealId)
        {
            var meal = db.Meals
                .Where(x => x.Id == mealId).FirstOrDefault();

            return meal;
        }

        public ShoppingCartViewModel GetShoppingCart(string userId)
        {
            var shoppingCart = db.User
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
                       }).ToList()
                   }).ToList()
               })
               .FirstOrDefault();

            return shoppingCart;
        }

        public IEnumerable<UserMealsViewModel> Products(string userId)
        {
            var user = GetById(userId);

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

        public void RemoveProduct(Data.Models.User user, Meal meal)
        {
            if (meal.Count > 1)
            {
                meal.Count--;
                db.SaveChanges();
            }
            else
            {
                user.ShoppingCart.Meals.Remove(meal);
                db.SaveChanges();
            }
        }

        public void SaveProduct(Data.Models.User user, Meal meal)
        {
            user.ShoppingCart.Meals.Add(meal);
            db.SaveChanges();
        }
    }
}
