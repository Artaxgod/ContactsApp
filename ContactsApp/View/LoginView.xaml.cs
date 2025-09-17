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
using ContactsApp.Services;
using Microsoft.Win32;

namespace ContactsApp.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly AuthController _authController;

        public User CurrentUser { get; private set; }

        public LoginView()
        {
            InitializeComponent();
            _authController = new AuthController();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailTextBox.Text.Trim();

            if (!Services.ValidationService.ValidateEmail(email))
            {
                ShowError("Введите корректный email");
                return;
            }

            CurrentUser = _authController.Login(email);
            if (CurrentUser != null)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                ShowError("Пользователь не найден");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerView = new RegisterView();
            if (registerView.ShowDialog() == true)
            {
                EmailTextBox.Text = registerView.RegisteredEmail;
            }
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }
    }
}
