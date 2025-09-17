using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public CategoryType Category { get; set; }
        public string Notes { get; set; }

        public string FullName => $"{LastName} {FirstName}";
    }
}
