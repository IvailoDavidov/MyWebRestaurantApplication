using MyTested.AspNetCore.Mvc;
using MyWebRestaurantApplication.Controllers;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Menu;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyWebRestaurantApplication.Test.Controllers
{
    public class MenuControllerTest
    {
        [Fact]
        public void CategoryMealsShouldReturnCorrectViewModel()
            => MyMvc
            .Pipeline()
            .ShouldMap("/Menu/CategoryMeals/5")
            .To<MenuController>(m => m.CategoryMeals(5))
            .Which(controller => controller
            .WithData(Enumerable.Range(0,5).Select(i => new CategoryMeal { Id = i += 1 })))
            .ShouldReturn()
            .View(v => v.WithModelOfType<ICollection<MealViewModel>>()
            .Passing(m => m.Count == 0));


        [Fact]
        public void MealsShouldReturnsAllCreatedCategories()
            => MyMvc
            .Pipeline()
            .ShouldMap("/Menu/Meals")
            .To<MenuController>(m => m.Meals())
            .Which(controller => controller
            .WithData(Enumerable.Range(0, 3).Select(i => new CategoryMeal { })))
            .ShouldReturn()
            .View(v => v.WithModelOfType<IEnumerable<CategoryViewModel>>()
            .Passing(m => m.Count() == 3));

        [Fact]
        public void DetailsShouldReturnAllInformationAboutMealWithTheRightId()
        => MyMvc
        .Pipeline()
        .ShouldMap("/Menu/Details/4")
        .To<MenuController>(m => m.Details(4))
        .Which(controller => controller
        .WithData(Enumerable.Range(0, 1).Select(i => new Meal { Name = "Pasta", Id = 4 })))
        .ShouldReturn()
        .View(v => v.WithModelOfType<MealViewModel>()
        .Passing(m => m.Name == "Pasta"));
    }
}
