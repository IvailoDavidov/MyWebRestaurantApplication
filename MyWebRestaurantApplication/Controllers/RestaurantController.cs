using Microsoft.AspNetCore.Mvc;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Models.Restaurant;
using System.Linq;

namespace MyWebRestaurantApplication.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext db;

        public RestaurantController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Gallery()
        {
            var categories = db.Categories.Select(x => new CategoryViewModel
            {
                Name = x.Name,
                PictureUrl = x.PictureUrl                
            }).ToList();
            return View(categories);           
        }
    }
}
