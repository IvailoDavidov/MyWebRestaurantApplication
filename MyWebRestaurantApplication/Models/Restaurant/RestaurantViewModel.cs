using MyWebRestaurantApplication.Data.Models;

namespace MyWebRestaurantApplication.Models.Restaurant
{
    public class RestaurantViewModel
    {
   
        public string StartWork { get; set; }

        public string FinishWork { get; set; }
       
        public Menu Menu { get; set; }
      
        public string Adress { get; set; }
    
        public string PhoneNumber { get; set; }
    }
}
