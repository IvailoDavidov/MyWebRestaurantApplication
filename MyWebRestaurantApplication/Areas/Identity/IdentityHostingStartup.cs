using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MyWebRestaurantApplication.Areas.Identity.IdentityHostingStartup))]
namespace MyWebRestaurantApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}