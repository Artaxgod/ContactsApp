using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsApp.Model;
using System.Windows;

namespace ContactsApp.Services
{
    public static class NotificationService
    {
        public static void ShowNotification(string message)
        {
            MessageBox.Show(message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void CheckUpcomingEvents(System.Collections.Generic.List<Event> events)
        {
            foreach (var eventItem in events.Where(e => !e.IsCompleted))
            {
                var timeUntilEvent = eventItem.EventDate - System.DateTime.Now;
                if (timeUntilEvent.TotalDays <= eventItem.ReminderDaysBefore &&
                    timeUntilEvent.TotalHours <= eventItem.ReminderHoursBefore)
                {
                    ShowNotification($"Напоминание: {eventItem.Title}\nДата: {eventItem.EventDate:dd.MM.yyyy}");
                }
            }
        }
    }
}
