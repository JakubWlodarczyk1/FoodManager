using FoodManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodManager.MVC.Extensions
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Stores a notification in TempData to be displayed to the user.
        /// </summary>
        /// <param name="controller">The current controller instance.</param>
        /// <param name="type">The type of the notification.</param>
        /// <param name="message">The message to display in the notification.</param>
        public static void SetNotification(this Controller controller, NotificationType type, string message)
        {
            var notification = new Notification(type, message);
            controller.TempData["Notification"] = JsonConvert.SerializeObject(notification);
        }
    }
}
