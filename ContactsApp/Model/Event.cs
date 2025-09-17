using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Model
{
    public class Event
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ContactId { get; set; }
        public string ContactName { get; set; }
        public System.DateTime EventDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReminderDaysBefore { get; set; }
        public int ReminderHoursBefore { get; set; }
        public bool IsCompleted { get; set; }

        public string ReminderText
        {
            get
            {
                if (ReminderDaysBefore > 0)
                    return $"{ReminderDaysBefore} день до события";
                if (ReminderHoursBefore > 0)
                    return $"{ReminderHoursBefore} часа до события";
                return "В день события";
            }
        }
    }
}
