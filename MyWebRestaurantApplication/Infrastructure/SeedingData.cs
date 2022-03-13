using Microsoft.AspNetCore.Identity;
using MyWebRestaurantApplication.Data;
using MyWebRestaurantApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Infrastructure
{
    public class SeedingData
    {     
        public  async Task CreateAdministrator(ApplicationDbContext db,
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
          
            var role = new IdentityRole { Name = "Administrator" };
            await roleManager.CreateAsync(role);

            var user = new IdentityUser
            {
                UserName = "Administrator",
                Email = "admin.administrator@abv.bg",
                PasswordHash = "administrator123"
            };
            await userManager.AddToRoleAsync(user, role.Name);
            await db.SaveChangesAsync();
        }

        public void Seeding(ApplicationDbContext db)
        {
            if (!db.Menu.Any())
            {

                var menu = new Menu
                {
                    Categories = new List<CategoryMeal>
                        {
                           new CategoryMeal
                           {
                               Name = "Soup",
                               Meals = new List<Meal>
                               {
                                 new Meal
                                 {
                                     Name = "Chicken Soup",
                                     Price = 3.80M,
                                     PictureUrl = "https://www.ambitiouskitchen.com/wp-content/uploads/2018/02/chickensoup-2-725x725-1.jpg" ,
                                 },
                                 new Meal
                                 {
                                     Name = "Mushroom Soup",
                                     Price = 3.40M,
                                     PictureUrl = "https://www.billyparisi.com/wp-content/uploads/2021/02/mushroom-soup-1.jpg",
                                 }

                               }.ToList(),
                               PictureUrl = "https://images.immediate.co.uk/production/volatile/sites/30/2021/03/Classic-Minestrone-Soup-13720e5.jpg",

                           },
                           new CategoryMeal
                           {
                               Name = "Salad",
                               Meals = new List<Meal>
                               {
                                 new Meal
                                 {
                                     Name = "Shopska Salad",
                                     Price = 4.60M,
                                     PictureUrl = "https://diethood.com/wp-content/uploads/2015/07/Shopska-Salad-Macedonian-Chopped-Salad-500x500.jpg",
                                     Ingredients = new List<Ingredient>
                                     {
                                         new Ingredient{Name ="Tomatoes"},
                                         new Ingredient{Name ="Cucumber"},
                                         new Ingredient{Name ="Cheese"},
                                         new Ingredient{Name ="Pepper"},
                                         new Ingredient{Name ="Onnion"}
                                     }
                                 },
                                 new Meal
                                 {
                                     Name = "Ovcharska Salad",
                                     Price = 5.20M,
                                     PictureUrl = "https://media-cdn.tripadvisor.com/media/photo-s/10/56/9b/e8/ovcharska-salad.jpg",
                                     Ingredients = new List<Ingredient>
                                     {
                                         new Ingredient{Name ="Tomatoes"},
                                         new Ingredient{Name ="Cucumber"},
                                         new Ingredient{Name ="Onion"},
                                         new Ingredient{Name ="Ham"},
                                         new Ingredient{Name ="Egg"},
                                         new Ingredient{Name ="Mushrooms"},
                                         new Ingredient{Name ="Peppers"},
                                     }
                                 }
                               }.ToList(),
                               PictureUrl = "https://insanelygoodrecipes.com/wp-content/uploads/2021/07/Lentil-Salad-with-Cherry-Tomatoes-and-Onions.jpg"
                           },
                           new CategoryMeal
                           {
                               Name = "Pasta",
                               Meals = new List<Meal>
                               {
                                  new Meal
                                  {
                                      Name = "Tagliatelle Carbonara",
                                      Price = 6.20M,
                                      PictureUrl = "https://skinnyspatula.com/wp-content/uploads/2021/10/Tagliatelle_Carbonara2.jpg"
                                  },
                                  new Meal
                                  {
                                      Name = "Penne All'Arrabbiata",
                                      Price = 6.90M,
                                      PictureUrl = "https://media-cdn.greatbritishchefs.com/media/p4rlqc5t/img72638.jpg?mode=crop&width=768&height=512"
                                  }
                               }.ToList(),
                               PictureUrl = "https://matekitchen.com/wp-content/uploads/2021/05/pasta-na-furna.jpg"
                           },
                           new CategoryMeal
                           {
                              Name = "BBQ",
                              Meals = new List<Meal>
                              {
                                 new Meal
                                 {
                                     Name = "Grilled Rib",
                                     Price = 9.90M,
                                     PictureUrl = "https://www.dadcooksdinner.com/wp-content/uploads/2017/09/Grilled-Short-Ribs-with-Smoked-Spanish-Paprika-Rub-P1004182-.jpg"
                                 },
                                 new Meal
                                 {
                                     Name = "Beef Steak",
                                     Price = 12.90M,
                                     PictureUrl = "https://lotusgrill.de/fileadmin/_processed_/4/c/csm_bild_rezeptideen1_972d668827.jpeg"
                                 }
                              }.ToList(),
                              PictureUrl = "https://media-cdn.tripadvisor.com/media/photo-s/12/ba/e3/61/getlstd-property-photo.jpg"
                           },
                           new CategoryMeal
                           {
                              Name = "Pizza",
                              Meals = new List<Meal>
                              {
                                 new Meal
                                 {
                                     Name = "Haway",
                                     Price = 7.60M,
                                     PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQcRr4p-CzBAO3xRAW-8Bd4EC1GLZDWPM9O9Q&usqp=CAU",
                                     Ingredients = new List<Ingredient>
                                     {
                                         new Ingredient{Name ="Tomato sos"},
                                         new Ingredient{Name ="Ham"},
                                         new Ingredient{Name ="Pine-apple"},
                                         new Ingredient{Name ="Yellow cheese"},                                       
                                     }
                                 },
                                 new Meal
                                 {
                                     Name = "Four Cheeses",
                                     Price = 6.70M,
                                     PictureUrl = "http://images.summitmedia-digital.com/yummyph/images/2017/01/16/four-cheese-pizza.jpg",
                                     Ingredients = new List<Ingredient>
                                     {
                                         new Ingredient{Name ="Tomato sos"},
                                         new Ingredient{Name ="Yellow cheese"},
                                         new Ingredient{Name ="Emental cheese"},
                                         new Ingredient{Name ="Blue cheese"},
                                         new Ingredient{Name ="Gauda"},                                    
                                         new Ingredient{Name ="Olives"},
                                         new Ingredient{Name ="Oil"}                                   
                                     }
                                 }
                              }.ToList(),
                              PictureUrl = "https://www.restu.cz/ir/restaurant/0fb/0fbbdcbeeff1582059fc88b59629da97.jpeg"
                           },
                           new CategoryMeal
                           {
                               Name = "Traditional",
                              Meals = new List<Meal>
                              {
                                 new Meal
                                 {
                                     Name = "Mish-Mash", 
                                     Price = 5.50M,
                                     PictureUrl = "https://m.1001recepti.com/images/photos/recipes/size_5/menemen-mish-mash-po-turski-8d730effe6c0b44d1d6be4543ea50900-[110065].jpg"
                                 },
                                 new Meal 
                                 {
                                     Name = "Moussaka",
                                     Price = 5.20M,
                                     PictureUrl = "https://d1bvpoagx8hqbg.cloudfront.net/originals/greek-mousaka-recipe-645ecca5141420ebbdc990dede6cf278.jpg"
                                 }
                              }.ToList(),
                              PictureUrl = "https://st4.depositphotos.com/1570716/20585/i/1600/depositphotos_205859402-stock-photo-indonesian-or-javanese-traditional-food.jpg"
                           },
                           new CategoryMeal
                           {
                              Name = "Dessert",
                              Meals = new List<Meal>
                              {
                                 new Meal 
                                 {
                                     Name = "Chocolate mousse", 
                                     Price = 4.60M, 
                                     PictureUrl = "https://www.cookingclassy.com/wp-content/uploads/2020/02/chocolate-mousse-3-500x500.jpg" 
                                 },
                                 new Meal 
                                 { 
                                     Name = "Tiramissu",
                                     Price = 5.20M,
                                     PictureUrl = "https://img-cdn.dnes.bg/d/images/photos/0513/0000513998-fbh.jpg" 
                                 }
                              }.ToList(),
                              PictureUrl = "https://images.immediate.co.uk/production/volatile/sites/38/2020/02/Gingerbread-souffle-ff41195.jpg?quality=90&resize=768,574"
                           },
                        }

                };
                db.Menu.Add(menu);
                db.SaveChanges();

            }
        }
    }
}
