using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebRestaurantApplication.Areas.Admin.Models.Menu
{
    public class MealAddEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 3)]
        public string Name { get; set; }
      
        [Range(0.1, 500)]
        public decimal Price { get; set; }

        [Range(0.1, 2000)]
        public float TotalGram { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<CategoriesViewModel> Categories { get; set; }
    }
}
