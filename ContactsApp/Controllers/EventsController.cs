using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsApp.Model;
using ContactsApp.Services;

namespace ContactsApp.Controllers
{
    public class EventsController
    {
        private readonly DatabaseService _dbService;

        public EventsController()
        {
            _dbService = new DatabaseService();
        }

        public List<Event> GetEvents(int userId)
        {
            return _dbService.GetEvents(userId);
        }

        public bool AddEvent(Event eventItem)
        {
            return _dbService.AddEvent(eventItem);
        }

        public bool CompleteEvent(int eventId)
        {
            return _dbService.CompleteEvent(eventId);
        }

        public void CheckUpcomingEvents(int userId)
        {
            var events = GetEvents(userId);
            NotificationService.CheckUpcomingEvents(events);
        }
    }
}
