namespace FoodManager.MVC.Models
{
    public enum NotificationType
    {
        Success,
        Info,
        Warning,
        Error
    }

    public class Notification(NotificationType type, string message)
    {
        public NotificationType Type { get; set; } = type;
        public string Message { get; set; } = message;
    }
}
