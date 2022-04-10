using Microsoft.AspNetCore.Authorization;
using MyTested.AspNetCore.Mvc;
using MyWebRestaurantApplication.Areas.Admin.Controllers;
using MyWebRestaurantApplication.Areas.Admin.Models.Menu;
using MyWebRestaurantApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyWebRestaurantApplication.Test.Area.Admin.Controllers
{
    public class MenuControllerTest
    {
        [Fact]
        public void AddMealGet_MethodShouldBeAuthorizedForAdminOnlyAndReturnCorrectView()
        {
            MyController<MenuController>
                 .Instance(controler => controler
                 .WithUser(usr => usr.InRole("Administrator"))
                 .WithData(Enumerable.Range(0, 1).Select(i => new CategoryMeal { Id = 5 })))
                 .Calling(x => x.AddMeal())
                 .ShouldHave()
                 .ActionAttributes(atr => atr.PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Administrator"))
                 .AndAlso()
                 .ShouldReturn()
                 .View(v => v.WithModelOfType<MealAddEditViewModel>()
                 .Passing(m => m.CategoryId = 5));
        }

        [Theory]
        [InlineData("Tomato", 5)]
        public void AddMealPost_ShouldAddMealAndAuthorizeForAdminThenAutorizePostMethodAndRedirectToAction(string mealName, int mealId)
        {
            MyController<MenuController>
                .Instance(controller => controller
                .WithUser(usr => usr.InRole("Administrator")))
                .Calling(c => c.AddMeal(new MealAddEditViewModel
                {
                    Name = "Tomato",
                    Id = 5,
                    Price = 5.50m,
                    TotalGram = 350,
                    CategoryId = 3,
                    PictureUrl = "https://www.ambitiouskitchen.com/wp-content/uploads/2018/02/chickensoup-2-725x725-1.jpg"
                }))
                .ShouldHave()
                .ActionAttributes(atr => atr.PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Administrator"))
                .ActionAttributes(atr => atr.RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(db => db
                .WithSet<Meal>(m =>
                {
                    m.Any(x => x.Id == mealId && x.Name == mealName);

                }))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Meals", "Menu", new { area = "" });
        }

        [Fact]
        public void EditMealGet_ShouldBeAuthorizedForAdminOnlyAndReturnCorrectView()
        {
            MyController<MenuController>
                .Instance(controler => controler
                .WithUser(usr => usr.InRole("Administrator"))
                .WithData(Enumerable.Range(0, 1).Select(i => new CategoryMeal { Id = 5, Meals = new List<Meal> { new Meal { Id = 7, Name = "tomato" } }})))
                .Calling(x => x.EditMeal(7))
                .ShouldHave()
                .ActionAttributes(atr => atr.PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Administrator"))              
                .AndAlso()
                .ShouldReturn()
                .View(v => v.WithModelOfType<MealAddEditViewModel>()
                .Passing(m => m.Name == "tomato"));
                             
        }

        [Theory]
        [InlineData("Tomato", 5)]
        public void EditMealPost_ShouldEditMealAndAuthorizeForAdminThenAutorizePostMethodAndRedirectToAction(string mealName, int mealId)
        {
            MyController<MenuController>
                .Instance(controller => controller
                .WithUser(usr => usr.InRole("Administrator"))
                .WithData(Enumerable.Range(0, 1).Select(i => new Meal { Id = 7, Name = "tomato" })))
                .Calling(c => c.EditMeal(7, new MealAddEditViewModel
                {
                    Name = mealName,
                    Id = mealId,
                    Price = 5.50m,
                    TotalGram = 350,
                    CategoryId = 5,
                    PictureUrl = "https://www.ambitiouskitchen.com/wp-content/uploads/2018/02/chickensoup-2-725x725-1.jpg",                   
                }))
                .ShouldHave()
                .ActionAttributes(atr => atr.PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Administrator"))
                .ActionAttributes(atr => atr.RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(db => db
                .WithSet<Meal>(m =>
                {
                    m.Any(x => x.Id == mealId && x.Name == mealName);

                }))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Meals", "Menu", new { area = "" });
        }


        [Theory]
        [InlineData(3)]
        public void DeleteMeal_ShouldCheckAuthorizeForAdminDeleteCorrectMealAndRedirectToAction(int mealId)
        {
            MyController<MenuController>
                 .Instance(controler => controler
                 .WithUser(usr => usr.InRole("Administrator"))
                 .WithData(Enumerable.Range(0, 1).Select(i => new Meal {Id = 3, Name = "tomato"})))
                 .Calling(x => x.DeleteMeal(mealId))
                 .ShouldHave()
                 .ActionAttributes(atr => atr.PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Administrator"))
                 .Data(data => data
                 .WithSet<Meal>(m => 
                 {
                     m.Any(x => x.Count < 1);
                 }))
                 .AndAlso()
                 .ShouldReturn()
                 .RedirectToAction("Meals", "Menu", new { area = "" });
        }
    }
}
