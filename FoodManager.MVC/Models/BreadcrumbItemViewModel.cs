namespace FoodManager.MVC.Models
{
    public class BreadcrumbItemViewModel
    {
        public string Title { get; set; } = default!;
        public string? Url { get; set; }

        public bool IsActive => string.IsNullOrWhiteSpace(Url);
    }
}
