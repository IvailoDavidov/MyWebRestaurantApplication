using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Services.Menu;
using System.Linq;
using Xunit;

namespace MyWebRestaurantApplication.Test.Services
{
    public class MenuServiceTests
    {

        [Fact]
        public void CategoryIdReturnsTheRightCategoryId()
        {
            var db = DataBaseMock.Instance;
            int categoryId = 3;

            var category = new CategoryMeal { Id = 3 };
            var category2 = new CategoryMeal { Id = 4 };
            var category3 = new CategoryMeal { Id = 5 };
            
            db.Categories.AddRange(category, category2, category3);
            db.SaveChanges();

            var menuService = new MenuService(db);

            var result = menuService.CategoryId(categoryId);

            Assert.Same(category, result);

        }

        [Fact]
        public void CategoryIdReturnsNullIfCategoryIsFalse()
        {
            var db = DataBaseMock.Instance;
           
            var category = new CategoryMeal { Id = 3 };
            var category2 = new CategoryMeal { Id = 4 };
            var category3 = new CategoryMeal { Id = 5 };

            db.Categories.AddRange(category, category2, category3);
            db.SaveChanges();

            var menuService = new MenuService(db);

            var result = menuService.CategoryId(7);

            Assert.Null(result);
        }

        [Fact]
        public void CategoriesReturnsAllCategoriesInDB()
        {
            var db = DataBaseMock.Instance;
            var category = new CategoryMeal { Id = 3 };
            var category2 = new CategoryMeal { Id = 4 };
            var category3 = new CategoryMeal { Id = 5 };
            

            db.Categories.AddRange(category, category2, category3);
            db.SaveChanges();

            var menuService = new MenuService(db);
            var result = menuService.Categories();

            Assert.True(result.Count() == db.Categories.Count());           
        }

        [Fact]
        public void CategoriesReturnsEmptyIfZeroCategoriesInDb()
        {
            var db = DataBaseMock.Instance;
            var menuService = new MenuService(db);
            var result = menuService.Categories();

            Assert.Empty(result);
        }

        [Fact]
        public void MealsByCategoryShouldReturnMealsInTheChosenCategory()
        {

            var db = DataBaseMock.Instance;
            var menuService = new MenuService(db);

            CategoryMeal category = new CategoryMeal { Name = "Traditional", Id = 2 };
            db.Categories.Add(category);

            Meal meal = new Meal {Id = 1, Name = "musaka", CategoryId = 2 };
            Meal meal2 = new Meal {Id = 2, Name = "mish-mash", CategoryId = 2};

            db.Meals.Add(meal);
            db.Meals.Add(meal2);
           
            category.Meals.Add(meal);
            category.Meals.Add(meal2);

            db.SaveChanges();

            var result = menuService.MealsByCategory(2);

            Assert.True(result.Count == 2);           
        }

        [Fact]
        public void MealsByCategoryShouldReturnZeroMeals()
        {
            var db = DataBaseMock.Instance;
            var menuService = new MenuService(db);

            CategoryMeal category = new CategoryMeal { Name = "Traditional", Id = 2 };
            db.Categories.Add(category);

            Meal meal = new Meal { Id = 1, Name = "musaka", CategoryId = 4 };
            Meal meal2 = new Meal { Id = 2, Name = "mish-mash", CategoryId = 4 };

            db.Meals.Add(meal);
            db.Meals.Add(meal2);

            db.SaveChanges();

            var result = menuService.MealsByCategory(3);
            Assert.Empty(result);
        }

        [Fact]
        public void DetailsShouldReturnTheRightMealDetails()
        {
            var db = DataBaseMock.Instance;
            var menuService = new MenuService(db);

            Meal meal = new Meal { Id = 5, Name = "Haway"};
            Meal meal2 = new Meal { Id = 2, Name = "Pasta"};

            db.Meals.AddRange(meal, meal2);
            db.SaveChanges();

            var result = menuService.Details(5);

            Assert.Equal("Haway", result.Name);
        }

        [Fact]
        public void DetailsShouldReturnNullIfMealIsMissing()
        {

            var db = DataBaseMock.Instance;
            var menuService = new MenuService(db);

            Meal meal = new Meal { Id = 5, Name = "Haway" };
            Meal meal2 = new Meal { Id = 2, Name = "Pasta" };

            db.Meals.AddRange(meal, meal2);
            db.SaveChanges();

            var result = menuService.Details(3);

            Assert.Null(result);
        }
    }
}
