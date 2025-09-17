using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactsApp.Services
{
    public static class ValidationService
    {
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return false;

            return Regex.IsMatch(phone, @"^\+?[0-9]{10,15}$");
        }

        public static bool ValidateRequiredFields(params string[] fields)
        {
            return fields.All(field => !string.IsNullOrWhiteSpace(field));
        }
    }
}
