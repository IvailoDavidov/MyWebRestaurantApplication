using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Cart;
using MyWebRestaurantApplication.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Services.User
{
    public interface IUserService
    {
        public Task<Data.Models.User> GetById(string Id);

        public Task<IEnumerable<UserMealsViewModel>> Products(string userId);

        public Task<Meal> GetMealById(int mealId);

        public Task<ShoppingCartViewModel> GetShoppingCart(string userId);

        public Task SaveProduct(Data.Models.User user, Meal meal);

        public Task RemoveProduct(Data.Models.User user, Meal meal);
       
    }
}
