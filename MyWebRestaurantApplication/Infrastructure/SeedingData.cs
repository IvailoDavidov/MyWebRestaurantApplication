using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWebRestaurantApplication.Infrastructure
{
    public class SeedingData
    {
        public void Seeding(ApplicationDbContext db)
        {
            if (!db.Restaurant.Any())
            {
                var restaurant = new Restaurant
                {
                    StartWork = "11:00",
                    FinishWork = "23:00",
                    Adress = "Sofia, bulevard ...",
                    PhoneNumber = "+359 ...",
                    Menu = new Menu
                    {
                        Categories = new List<CategoryMeal>
                        {
                           new CategoryMeal
                           {
                               Name = "Soup",
                               Meals = new List<Meal>
                               { new Meal { Name = "Chicken Soup", Price = 2.80 },
                                 new Meal { Name = "Mushroom Soup", Price = 2.40}
                           }.ToList(),
                           PictureUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/homemade-pumpkin-soup-royalty-free-image-1571855802.jpg?crop=0.667xw:1.00xh;0.333xw,0&resize=480:*"

                          },

                           new CategoryMeal{Name = "Salad"},
                           new CategoryMeal{Name = "Pasta"},
                           new CategoryMeal{Name = "BBQ"},
                           new CategoryMeal{Name = "Pizza"},
                           new CategoryMeal{Name = "Traditional"},
                           new CategoryMeal{Name = "Dessert"},
                        }
                    }
                };
                db.Restaurant.Add(restaurant);
                db.SaveChanges();
            }
        }
    }
}
