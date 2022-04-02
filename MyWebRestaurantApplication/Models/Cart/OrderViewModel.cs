using System;
using System.ComponentModel.DataAnnotations;

namespace MyWebRestaurantApplication.Models.Cart
{
    public class OrderViewModel
    {    
        public string UserId { get; set; }

        public Data.Models.User User { get; set; }

        [StringLength(100)]
        public string UserAdress { get; set; }

        public DateTime DateTime { get; set; }
    }
}
