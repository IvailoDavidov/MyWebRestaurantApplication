using MyWebRestaurantApplication.Data.Models;
using System.Collections.Generic;

namespace MyWebRestaurantApplication.Areas.Admin.Models
{
    public class MealAddViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public float TotalGram { get; set; }

        public string PictureUrl { get; set; }

        public int CategoryId { get; set; }

        public ICollection<CategoriesViewModel> Categories { get; set; }

    }
}
