using MyWebRestaurantApplication.Models.Cart;

namespace MyWebRestaurantApplication.Services.Cart
{
    public interface ICartService
    {
        public void CreateOrder(OrderViewModel order, string userId);

        public void Clear(Data.Models.User user);
    }
}
