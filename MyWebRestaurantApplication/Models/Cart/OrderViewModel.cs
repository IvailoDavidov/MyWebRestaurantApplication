using System;

namespace MyWebRestaurantApplication.Models.Cart
{
    public class OrderViewModel
    {    
        public string UserId { get; set; }

        public Data.Models.User User { get; set; }

        public DateTime DateTime { get; set; }
    }
}
