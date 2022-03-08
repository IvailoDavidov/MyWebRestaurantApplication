using System;

namespace MyWebRestaurantApplication.Data.Models
{
    public class Restaurant
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string StartWork { get; set; }

        public string FinishWork { get; set; }

        public Menu Menu { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }
    }
}
