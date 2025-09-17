using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsApp.Model;
using ContactsApp.Services;

namespace ContactsApp.Controllers
{
    public class ContactsController
    {
        private readonly DatabaseService _dbService;

        public ContactsController()
        {
            _dbService = new DatabaseService();
        }

        public List<Contact> GetContacts(int userId)
        {
            return _dbService.GetContacts(userId);
        }

        public bool AddContact(Contact contact)
        {
            return _dbService.AddContact(contact);
        }

        public bool UpdateContact(Contact contact)
        {
            return _dbService.UpdateContact(contact);
        }

        public bool DeleteContact(int contactId)
        {
            return _dbService.DeleteContact(contactId);
        }

        public List<Contact> SearchContacts(int userId, string searchText, string category)
        {
            var contacts = GetContacts(userId);

            if (!string.IsNullOrEmpty(searchText))
            {
                contacts = contacts.FindAll(c =>
                    c.FullName.Contains(searchText) ||
                    c.Company.Contains(searchText) ||
                    c.Email.Contains(searchText));
            }

            if (!string.IsNullOrEmpty(category) && category != "Все")
            {
                contacts = contacts.FindAll(c => c.Category.ToString() == category);
            }

            return contacts;
        }
    }
}
