using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebRestaurantApplication.Data.Models
{
    public class Meal
    {
        public Meal()
        {
            Ingredients = new HashSet<Ingredient>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
       
        public float TotalGram { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public CategoryMeal CategoryMeal { get; set; }

        public int Count { get; set; }

        public ICollection<ShoppingCart> ShoppingCarts { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
