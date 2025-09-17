using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsApp.Model;

namespace ContactsApp.Controllers
{
    public class MainController
    {
        public User CurrentUser { get; private set; }
        public AuthController AuthController { get; }
        public ContactsController ContactsController { get; }
        public EventsController EventsController { get; }

        public MainController()
        {
            AuthController = new AuthController();
            ContactsController = new ContactsController();
            EventsController = new EventsController();
        }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
