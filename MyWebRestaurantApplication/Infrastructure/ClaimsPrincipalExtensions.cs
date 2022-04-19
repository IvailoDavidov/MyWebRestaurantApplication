using System.Security.Claims;
using System.Threading.Tasks;

namespace MyWebRestaurantApplication.Infrastructure
{
    public static  class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return  user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
