using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Data;

namespace MyWebRestaurantApplication.Controllers
{
    public class EventController : Controller
    {
       

        public IActionResult ChildParty()
        {

            return View();
        }

        public IActionResult WeddingVenue()
        {
            return View();          
        }

        
        public IActionResult BanquetFeast() 
        {

            return View();
        }
    }
}
