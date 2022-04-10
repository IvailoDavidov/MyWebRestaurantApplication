using MyTested.AspNetCore.Mvc;
using MyWebRestaurantApplication.Controllers;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.User;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyWebRestaurantApplication.Test.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public void MyProductsShouldGetUserAndShouldBeAuthorized()
        {
            MyPipeline
              .Configuration()
              .ShouldMap(req => req
              .WithPath("/User/MyProducts")
              .WithUser())
              .To<UserController>(c => c.MyProducts())
              .Which()
              .ShouldHave()
              .ActionAttributes(atr => atr.RestrictingForAuthorizedRequests());
        }

        [Theory]
        [InlineData("CartId", 5)]
        public void MyProductsShouldGetUserProductsAndReturnCorrectView(string cartId, int mealId)
        {      

            MyController<UserController>
              .Instance(controler => controler
              .WithUser()
              .WithData(GetUserWithData("TestId",cartId,mealId)))
              .Calling(x => x.MyProducts())
              .ShouldHave()
              .ActionAttributes(atr => atr.RestrictingForAuthorizedRequests())            
              .AndAlso()
              .ShouldReturn()
              .View(v => v.WithModelOfType<IEnumerable<UserMealsViewModel>>()
              .Passing(m =>
              {
                  Assert.True(m.Any());                 
              }));  
        }

        [Theory]
        [InlineData("CartId", 5)]
        public void AddProductShouldBeForAuthorizedShouldGetRightUserWithRightMealAndSaveToBaseThenRedirect(string cartId, int mealId)
        {

            MyController<UserController>
              .Instance(controler => controler           
              .WithUser()
              .WithData(GetUserWithData("TestId",cartId,mealId)))
              .Calling(x => x.AddProduct(5))
              .ShouldHave()
              .ActionAttributes(atr => atr.RestrictingForAuthorizedRequests())                    
              .AndAlso()              
              .ShouldReturn()
              .RedirectToAction("MyProducts", "User");                         
        }

        [Theory]
        [InlineData("CartId", 5)]
        public void RemoveProduct(string cartId, int mealId)
        {

            MyController<UserController>
              .Instance(controler => controler
              .WithUser()
              .WithData(GetUserWithData("TestId", cartId, mealId)))
              .Calling(x => x.RemoveProduct(5))
              .ShouldHave()
              .ActionAttributes(atr => atr.RestrictingForAuthorizedRequests())
              .AndAlso()
              .ShouldReturn()
              .RedirectToAction("MyProducts", "User");           
        }

        private static IEnumerable<User> GetUserWithData(string userId = "TestId", string cartId = "CartId", int mealId = 5)
         => Enumerable.Range(0, 1)
            .Select(i => 
            new User 
            { 
                Id = userId,
                ShoppingCart = new ShoppingCart
                {
                    Id = cartId,
                    Meals = new List<Meal>
                    {
                        new Meal 
                        { 
                            Id = mealId,
                            Name = "tomato" 
                        }
                    }
                } 
            });
    }
}

