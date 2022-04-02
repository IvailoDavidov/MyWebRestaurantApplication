using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MyWebRestaurantApplication.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string ShoppingCartId { get; set; } 

        public ShoppingCart ShoppingCart { get; set; }

        
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
