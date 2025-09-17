using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ContactsApp.Controllers;
using ContactsApp.Model;

namespace ContactsApp.View
{
    /// <summary>
    /// Логика взаимодействия для RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        private readonly AuthController _authController;
        public string RegisteredEmail { get; private set; }

        public RegisterView()
        {
            InitializeComponent();
            _authController = new AuthController();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var lastName = LastNameTextBox.Text.Trim();
            var firstName = FirstNameTextBox.Text.Trim();
            var email = EmailTextBox.Text.Trim();
            var phone = PhoneTextBox.Text.Trim();

            if (!Services.ValidationService.ValidateRequiredFields(lastName, firstName, email, phone))
            {
                ShowError("Все поля обязательны для заполнения");
                return;
            }

            if (!Services.ValidationService.ValidateEmail(email))
            {
                ShowError("Некорректный email");
                return;
            }

            if (!Services.ValidationService.ValidatePhone(phone))
            {
                ShowError("Некорректный телефон");
                return;
            }

            var user = new User
            {
                LastName = lastName,
                FirstName = firstName,
                Email = email,
                Phone = phone
            };

            if (_authController.Register(user))
            {
                RegisteredEmail = email;
                DialogResult = true;
                Close();
            }
            else
            {
                ShowError("Ошибка регистрации. Возможно, email уже используется.");
            }
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }
    }
}
