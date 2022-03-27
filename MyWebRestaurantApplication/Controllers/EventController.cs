using Microsoft.AspNetCore.Mvc;

namespace MyWebRestaurantApplication.Controllers
{
    public class EventController : Controller
    {
        public EventController()
        {

        }

        public IActionResult Events()
        {
            return View();
        }
    }
}
