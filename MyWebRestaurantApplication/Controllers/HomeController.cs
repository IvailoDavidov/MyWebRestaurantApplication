using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models;
using MyWebRestaurantApplication.Models.Menu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger)
        {
            this.db = db;
            this._logger = logger;
        }

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;
     

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            var categories = db.Categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                PictureUrl = x.PictureUrl

            }).ToList();
            return View(categories);
        }

        public IActionResult Events()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
