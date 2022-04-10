using Microsoft.EntityFrameworkCore;
using MyWebRestaurantApplication.Data;
using System;

namespace MyWebRestaurantApplication.Test
{
    public static class DataBaseMock
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

                return new ApplicationDbContext(dbContextOptions);

            }
        }
    }
}
