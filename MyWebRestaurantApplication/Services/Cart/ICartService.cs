using MyWebRestaurantApplication.Models.Cart;

namespace MyWebRestaurantApplication.Services.Cart
{
    public interface ICartService
    {
        public void CreateOrder(OrderViewModel order, Data.Models.User user);
        public void Clear(Data.Models.User user);
    }
}
