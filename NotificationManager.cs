using System;

namespace BilliardsGame
{
    public class Player
    {
        public string Name { get; set; }
        public bool NotificationsEnabled { get; private set; } = true;

        public void ToggleNotifications()
        {
            NotificationsEnabled = !NotificationsEnabled;
            Console.WriteLine($"Notifications are now {(NotificationsEnabled ? "enabled" : "disabled")} for {Name}.");
        }

        public void ReceiveNotification(string message)
        {
            if (NotificationsEnabled)
            {
                Console.WriteLine($"Notification for {Name}: {message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = new Player { Name = "John" };

            // Toggling notifications
            player1.ToggleNotifications();
            player1.ToggleNotifications();

            // Receiving a notification
            player1.ReceiveNotification("Daily login reward available!");

            // Disable notifications and test again
            player1.ToggleNotifications();
            player1.ReceiveNotification("Daily login reward available!");
        }
    }
}
