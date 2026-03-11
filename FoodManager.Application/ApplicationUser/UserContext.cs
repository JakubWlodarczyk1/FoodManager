using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FoodManager.Application.ApplicationUser
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        /// <summary>
        /// Retrieves basic information (id, email and roles) about the currently authenticated user.
        /// </summary>
        public CurrentUser GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("Context user is not present");
            }

            if (user.Identity is not { IsAuthenticated: true })
            {
                throw new InvalidOperationException("User is not authenticated");
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value);

            return new CurrentUser(id, email, roles);
        }
    }
}
