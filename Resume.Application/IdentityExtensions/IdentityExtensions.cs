using System.Security.Claims;
using System.Security.Principal;

namespace Resume.Application.IdentityExtensions
{
	public static class IdentityExtensions
	{
		public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
		{
			if (claimsPrincipal == null)
			{
				return default;
			}

			string? userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			return string.IsNullOrEmpty(userId) ? default : long.Parse(userId);
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
