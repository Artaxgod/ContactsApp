using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsApp.Model;

namespace ContactsApp.Services
{
    public class DatabaseService
    {
        private string connectionString;

        public DatabaseService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ContactsAppConnection"].ConnectionString;
        }

        // Users
        public bool RegisterUser(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Users (LastName, FirstName, Email, Phone) VALUES (@LastName, @FirstName, @Email, @Phone)",
                    connection);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Phone", user.Phone);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public User Login(string email)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Users WHERE Email = @Email", connection);
                command.Parameters.AddWithValue("@Email", email);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = (int)reader["Id"],
                            LastName = reader["LastName"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        // Contacts
        public List<Contact> GetContacts(int userId)
        {
            var contacts = new List<Contact>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Contacts WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contacts.Add(new Contact
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            LastName = reader["LastName"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Company = reader["Company"]?.ToString(),
                            Category = (Model.CategoryType)System.Enum.Parse(typeof(Model.CategoryType), reader["Category"].ToString()),
                            Notes = reader["Notes"]?.ToString()
                        });
                    }
                }
            }
            return contacts;
        }

        public bool AddContact(Contact contact)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    @"INSERT INTO Contacts (UserId, LastName, FirstName, Email, Phone, Company, Category, Notes) 
                      VALUES (@UserId, @LastName, @FirstName, @Email, @Phone, @Company, @Category, @Notes)",
                    connection);
                command.Parameters.AddWithValue("@UserId", contact.UserId);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@Email", contact.Email);
                command.Parameters.AddWithValue("@Phone", contact.Phone);
                command.Parameters.AddWithValue("@Company", contact.Company ?? (object)System.DBNull.Value);
                command.Parameters.AddWithValue("@Category", contact.Category.ToString());
                command.Parameters.AddWithValue("@Notes", contact.Notes ?? (object)System.DBNull.Value);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateContact(Contact contact)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    @"UPDATE Contacts SET LastName=@LastName, FirstName=@FirstName, Email=@Email, 
                      Phone=@Phone, Company=@Company, Category=@Category, Notes=@Notes WHERE Id=@Id",
                    connection);
                command.Parameters.AddWithValue("@Id", contact.Id);
                command.Parameters.AddWithValue("@LastName", contact.LastName);
                command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                command.Parameters.AddWithValue("@Email", contact.Email);
                command.Parameters.AddWithValue("@Phone", contact.Phone);
                command.Parameters.AddWithValue("@Company", contact.Company ?? (object)System.DBNull.Value);
                command.Parameters.AddWithValue("@Category", contact.Category.ToString());
                command.Parameters.AddWithValue("@Notes", contact.Notes ?? (object)System.DBNull.Value);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteContact(int contactId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Contacts WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", contactId);

                return command.ExecuteNonQuery() > 0;
            }
        }

        // Events
        public List<Event> GetEvents(int userId)
        {
            var events = new List<Event>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    @"SELECT e.*, c.FirstName + ' ' + c.LastName as ContactName 
                      FROM Events e LEFT JOIN Contacts c ON e.ContactId = c.Id 
                      WHERE e.UserId = @UserId ORDER BY e.EventDate",
                    connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new Event
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            ContactId = reader["ContactId"] as int?,
                            ContactName = reader["ContactName"]?.ToString() ?? string.Empty,
                            EventDate = (System.DateTime)reader["EventDate"],
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"]?.ToString() ?? string.Empty,
                            ReminderDaysBefore = (int)reader["ReminderDaysBefore"],
                            ReminderHoursBefore = (int)reader["ReminderHoursBefore"],
                            IsCompleted = (bool)reader["IsCompleted"]
                        });
                    }
                }
            }
            return events;
        }

        public bool AddEvent(Event eventItem)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    @"INSERT INTO Events (UserId, ContactId, EventDate, Title, Description, ReminderDaysBefore, ReminderHoursBefore, IsCompleted)
                      VALUES (@UserId, @ContactId, @EventDate, @Title, @Description, @ReminderDaysBefore, @ReminderHoursBefore, @IsCompleted)",
                    connection);
                command.Parameters.AddWithValue("@UserId", eventItem.UserId);
                command.Parameters.AddWithValue("@ContactId", (object)eventItem.ContactId ?? System.DBNull.Value);
                command.Parameters.AddWithValue("@EventDate", eventItem.EventDate);
                command.Parameters.AddWithValue("@Title", eventItem.Title);
                command.Parameters.AddWithValue("@Description", eventItem.Description ?? (object)System.DBNull.Value);
                command.Parameters.AddWithValue("@ReminderDaysBefore", eventItem.ReminderDaysBefore);
                command.Parameters.AddWithValue("@ReminderHoursBefore", eventItem.ReminderHoursBefore);
                command.Parameters.AddWithValue("@IsCompleted", eventItem.IsCompleted);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool CompleteEvent(int eventId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Events SET IsCompleted = 1 WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", eventId);

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
