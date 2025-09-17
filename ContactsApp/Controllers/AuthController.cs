using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsApp.Model;
using ContactsApp.Services;

namespace ContactsApp.Controllers
{
    public class AuthController
    {
        private readonly DatabaseService _dbService;

        public AuthController()
        {
            _dbService = new DatabaseService();
        }

        public User Login(string email)
        {
            return _dbService.Login(email);
        }

        public bool Register(User user)
        {
            return _dbService.RegisterUser(user);
        }
    }
}
