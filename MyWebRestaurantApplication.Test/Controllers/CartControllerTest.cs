using MyTested.AspNetCore.Mvc;
using MyWebRestaurantApplication.Controllers;
using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Models.Cart;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace MyWebRestaurantApplication.Test.Controllers
{
    public class CartControllerTest
    {
        [Theory]
        [InlineData("TestId","CartId", 7)]
        public void TotalShouldReturnCorrectDataWithCorrectView(string userId, string cartId, int mealId)
        {
            MyController<CartController>
              .Instance(controler => controler
              .WithUser()
              .WithData(GetUserWithData(userId,cartId,mealId)))
              .Calling(x => x.Total())
              .ShouldHave()
              .ActionAttributes(atr => atr.RestrictingForAuthorizedRequests())
              .AndAlso()
              .ShouldReturn()
              .View(v => v.WithModelOfType<ShoppingCartViewModel>()
              .Passing(c => c.Id == cartId && c.Meals.Any(x => x.Id == 7)));
        }


        [Fact]
        public void TotalShouldHaveUserAndBeingForAuthorizedUsers()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(req => req
                .WithPath("/Cart/Total")
                .WithUser())
                .To<CartController>(c => c.Total())
                .Which()
                .ShouldHave()
                .ActionAttributes(atr => atr.RestrictingForAuthorizedRequests());         
        }

        [Fact]
        public void ConfirmOrderShouldHaveUserAndBeingForAuthorizedUsers()
        {
            MyPipeline
              .Configuration()
              .ShouldMap(req => req
              .WithPath("/Cart/ConfirmOrder")
              .WithUser())
              .To<CartController>(c => c.ConfirmOrder(With.Any<OrderViewModel>()))
              .Which()
              .ShouldHave()
              .ActionAttributes(atr => atr.RestrictingForAuthorizedRequests());
        }

        [Theory]
        [InlineData("Sofia")]
        public void ConfirmOrderShouldCreateOrderAndCallTheRightModel(string userAdress)
        {
            MyController<CartController>
                .Instance(controller => controller
                .WithUser())
                .Calling(c => c.ConfirmOrder(new OrderViewModel
                {
                    UserAdress = userAdress
                }))
                .ShouldHave()
                .ActionAttributes(atr => atr
                .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(db => db
                .WithSet<Order>(orders =>
                {
                    orders.Any(o => o.UserId == TestUser.Identifier && o.UserAdress == userAdress);

                }));
        }

        [Fact]
        public void ConfirmOrderShouldGetUserShoppingCart()
        {

            MyController<CartController>
                 .Instance(controller => controller
                 .WithUser())
                 .Calling(c => c.ConfirmOrder(new OrderViewModel { }))
                 .ShouldHave()
                 .ActionAttributes(atr => atr
                 .RestrictingForAuthorizedRequests())
                 .ValidModelState()
                 .Data(db => db
                 .WithSet<ShoppingCart>(sc =>
                 {
                     sc.Any(sc => sc.UserId == TestUser.Identifier);

                 }));

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
