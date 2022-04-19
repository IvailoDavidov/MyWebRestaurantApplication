using MyWebRestaurantApplication.Data.Models;
using MyWebRestaurantApplication.Services.Menu;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyWebRestaurantApplication.Test.Services
{
    public class MenuServiceTests
    {

        [Fact]
        public async Task CategoryIdReturnsTheRightCategoryId()
        {
            var db = DataBaseMock.Instance;
            int categoryId = 3;

            var category = new CategoryMeal { Id = 3 };
            var category2 = new CategoryMeal { Id = 4 };
            var category3 = new CategoryMeal { Id = 5 };
            
            await db.Categories.AddRangeAsync(category, category2, category3);
            await db.SaveChangesAsync();

            var menuService = new MenuService(db);

            var result = await menuService.CategoryId(categoryId);

            Assert.Same(category, result);

        }

        [Fact]
        public async Task CategoryIdReturnsNullIfCategoryIsFalse()
        {
            var db = DataBaseMock.Instance;
           
            var category = new CategoryMeal { Id = 3 };
            var category2 = new CategoryMeal { Id = 4 };
            var category3 = new CategoryMeal { Id = 5 };

            await db.Categories.AddRangeAsync(category, category2, category3);
            await db.SaveChangesAsync();

            var menuService = new MenuService(db);

            var result = await menuService.CategoryId(7);

            Assert.Null(result);
        }

        [Fact]
        public async Task CategoriesReturnsAllCategoriesInDB()
        {
            var db = DataBaseMock.Instance;
            var category = new CategoryMeal { Id = 3 };
            var category2 = new CategoryMeal { Id = 4 };
            var category3 = new CategoryMeal { Id = 5 };
            

            await db.Categories.AddRangeAsync(category, category2, category3);
            await db.SaveChangesAsync();

            var menuService = new MenuService(db);
            var result = await menuService.Categories();

             Assert.True(result.Count() == db.Categories.Count());           
        }

        [Fact]
        public async Task CategoriesReturnsEmptyIfZeroCategoriesInDb()
        {
            var db = DataBaseMock.Instance;
            var menuService = new MenuService(db);
            var result = await menuService.Categories();

            Assert.Empty(result);
        }

        [Fact]
        public async Task MealsByCategoryShouldReturnMealsInTheChosenCategory()
        {

            var db = DataBaseMock.Instance;
            var menuService = new MenuService(db);

            CategoryMeal category = new CategoryMeal { Name = "Traditional", Id = 2 };
            await db.Categories.AddAsync(category);

            Meal meal = new Meal {Id = 1, Name = "musaka", CategoryId = 2 };
            Meal meal2 = new Meal {Id = 2, Name = "mish-mash", CategoryId = 2};

            await db.Meals.AddAsync(meal);
            await db.Meals.AddAsync(meal2);
           
            category.Meals.Add(meal);
            category.Meals.Add(meal2);

            await db.SaveChangesAsync();

            var result = await menuService.MealsByCategory(2);

            Assert.True(result.Count == 2);           
        }

        [Fact]
        public async Task MealsByCategoryShouldReturnZeroMeals()
        {
            var db = DataBaseMock.Instance;
            var menuService = new MenuService(db);

            CategoryMeal category = new CategoryMeal { Name = "Traditional", Id = 2 };
            await db.Categories.AddAsync(category);

            Meal meal = new Meal { Id = 1, Name = "musaka", CategoryId = 4 };
            Meal meal2 = new Meal { Id = 2, Name = "mish-mash", CategoryId = 4 };

            await db.Meals.AddAsync(meal);
            await db.Meals.AddAsync(meal2);

            await db.SaveChangesAsync();

            var result = await menuService.MealsByCategory(3);
            Assert.Empty(result);
        }

        [Fact]
        public async Task DetailsShouldReturnTheRightMealDetails()
        {
            var db = DataBaseMock.Instance;
            var menuService = new MenuService(db);

            Meal meal = new Meal { Id = 5, Name = "Haway"};
            Meal meal2 = new Meal { Id = 2, Name = "Pasta"};

            await db.Meals.AddRangeAsync(meal, meal2);
            await db.SaveChangesAsync();

            var result = await menuService.Details(5);

            Assert.Equal("Haway", result.Name);
        }

        [Fact]
        public async Task DetailsShouldReturnNullIfMealIsMissing()
        {

            var db = DataBaseMock.Instance;
            var menuService = new MenuService(db);

            Meal meal = new Meal { Id = 5, Name = "Haway" };
            Meal meal2 = new Meal { Id = 2, Name = "Pasta" };

           await db.Meals.AddRangeAsync(meal, meal2);
           await db.SaveChangesAsync();

            var result = await menuService.Details(3);

            Assert.Null(result);
        }
    }
}
