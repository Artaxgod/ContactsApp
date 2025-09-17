using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ContactsApp.View;

namespace ContactsApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginView = new LoginView();
            if (loginView.ShowDialog() == true && loginView.CurrentUser != null)
            {
                var mainView = new MainView(loginView.CurrentUser);
                mainView.Show();
            }
            else
            {
                Shutdown();
            }
        }
    }
}
