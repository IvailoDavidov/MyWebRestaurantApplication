using MyWebRestaurantApplication.Models.Menu;
using MyWebRestaurantApplication.Models.User;
using System.Collections.Generic;
using System.Linq;

namespace MyWebRestaurantApplication.Models.Cart
{
    public class ShoppingCartViewModel
    {
        public string Id { get; set; } 
       

        public ICollection<UserMealsViewModel> Meals { get; set; }

        public decimal TotalSum => Meals.Sum(x => x.Price * x.Count);
    }
}
