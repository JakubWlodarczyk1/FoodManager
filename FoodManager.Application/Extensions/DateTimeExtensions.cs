namespace FoodManager.Application.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Checks if the date is today or in the future.
        /// </summary>
        public static bool IsTodayOrInTheFuture(this DateTime date)
        {
            return date.Date >= DateTime.Now.Date;
        }
    }
}
