using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Data;

namespace MyWebRestaurantApplication.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext db;

        public EventController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Events()
        {
            return View();
        }

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
