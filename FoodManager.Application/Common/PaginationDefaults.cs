namespace FoodManager.Application.Common
{
    public static class PaginationDefaults
    {
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 10;
        public static readonly int[] AllowedPageSizes = [5, 10, 15, 30];
    }
}
