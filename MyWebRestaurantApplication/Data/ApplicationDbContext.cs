using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebRestaurantApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebRestaurantApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        
        public DbSet<Menu> Menu { get; set; }
        public DbSet<CategoryMeal> Categories { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }


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
                          

            base.OnModelCreating(builder);
        }
    }
}
