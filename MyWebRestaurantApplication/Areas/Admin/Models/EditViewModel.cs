using System.Collections.Generic;

namespace MyWebRestaurantApplication.Areas.Admin.Models
{
    public class EditViewModel
    {
        public string Name { get; set; }
      
        public decimal Price { get; set; }

        public float TotalGram { get; set; }

        public string PictureUrl { get; set; }

        public int CategoryId { get; set; }

        public ICollection<CategoriesViewModel> Categories { get; set; }
    }
}
