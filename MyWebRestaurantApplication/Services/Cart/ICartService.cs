using MyWebRestaurantApplication.Models.Cart;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Services.Cart
{
    public interface ICartService
    {
        public Task CreateOrder(OrderViewModel order, Data.Models.User user);
      
    }
}
