using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebRestaurantApplication.Data.Models;

namespace MyWebRestaurantApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Menu> Menu { get; set; }     
        public DbSet<CategoryMeal> Categories { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Meal>()
                .HasOne(m => m.CategoryMeal)
                .WithMany(c => c.Meals)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ingredient>()
                .HasMany(i => i.Meals)
                .WithMany(m => m.Ingredients);

            builder.Entity<User>()
                .HasOne(u => u.ShoppingCart)
                .WithOne(c => c.User)
                .HasForeignKey<User>(u => u.ShoppingCartId)
                .OnDelete(DeleteBehavior.Restrict);

         
                                    

            base.OnModelCreating(builder);
        }
    }
}
