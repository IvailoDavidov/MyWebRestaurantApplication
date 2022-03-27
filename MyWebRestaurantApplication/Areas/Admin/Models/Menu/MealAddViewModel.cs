using MyWebRestaurantApplication.Data.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyWebRestaurantApplication.Areas.Admin.Models.Menu
{
    public class MealAddViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength (30,MinimumLength = 2)]
        public string Name { get; set; }

       [Range(0.1, 500)]
        public decimal Price { get; set; }

        [Range(0.1, 2000)]
        public float TotalGram { get; set; }
      
        [Required]
        [Url]
        public string PictureUrl { get; set; }

        public int CategoryId { get; set; }


        public ICollection<CategoriesViewModel> Categories { get; set; }

    }
}
