using System;
using System.ComponentModel.DataAnnotations;

namespace MyWebRestaurantApplication.Data.Models
{
    public class Restaurant
    {
        [Required]
        [StringLength(100)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(10)]       
        public string StartWork { get; set; }

        [Required]
        [StringLength(10)]
        public string FinishWork { get; set; }

        [Required]
        public Menu Menu { get; set; }

        [Required]
        [StringLength(120)]
        public string Adress { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }
    }
}
