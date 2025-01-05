using System.Security.Claims;
using System.Security.Principal;

namespace Resume.Domain.IdentityExtensions
{
    public static class IdentityExtensions
    {
        public static long GetUserId(this ClaimsPrincipal claimPrincipal)
        {
            if (claimPrincipal == null)
            {
                return default;
            }

            string? userId = claimPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return string.IsNullOrWhiteSpace(userId) ? default : long.Parse(userId);
        }


        public static long GetUserId(this IPrincipal principal)
        {
            if (principal == null)
            {
                return default;
            }

            var user = (ClaimsPrincipal)principal;

            return user.GetUserId();
        }
    }
}
