using FoodManager.Domain.Constants;

namespace FoodManager.Application.ApplicationUser
{
    public class CurrentUser(string id, string email, IEnumerable<string> roles)
    {
        public string Id { get; set; } = id;
        public string Email { get; set; } = email;
        public IReadOnlyCollection<string> Roles { get; set; } = roles.ToArray();

        public bool IsInRole(string role)
            => Roles.Contains(role);
    }
}
