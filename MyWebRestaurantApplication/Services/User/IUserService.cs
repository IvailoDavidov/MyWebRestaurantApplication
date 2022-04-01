
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Cart;
using MyWebRestaurantApplication.Models.User;
using System.Collections.Generic;

namespace MyWebRestaurantApplication.Services.User
{
    public interface IUserService
    {
        public Data.Models.User GetById(string Id);

        public IEnumerable<UserMealsViewModel> Products(string userId);

        public Meal GetMealById(int mealId);

        public ShoppingCartViewModel GetShoppingCart(string userId);

        public void SaveProduct(Data.Models.User user,Meal meal);

        public void RemoveProduct(Data.Models.User user, Meal meal);
       
    }
}
