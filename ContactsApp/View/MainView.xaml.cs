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
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly MainController _mainController;
        private List<Contact> _allContacts;
        private List<Event> _allEvents;

        public MainView(User user)
        {
            InitializeComponent();
            _mainController = new MainController();
            _mainController.SetCurrentUser(user);

            LoadContacts();
            LoadEvents();
            CheckNotifications();
        }

        private void LoadContacts()
        {
            _allContacts = _mainController.ContactsController.GetContacts(_mainController.CurrentUser.Id);
            ContactsDataGrid.ItemsSource = _allContacts;
        }

        private void LoadEvents()
        {
            _allEvents = _mainController.EventsController.GetEvents(_mainController.CurrentUser.Id);
            EventsDataGrid.ItemsSource = _allEvents;
        }

        private void CheckNotifications()
        {
            _mainController.EventsController.CheckUpcomingEvents(_mainController.CurrentUser.Id);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterContacts();
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterContacts();
        }

        private void FilterContacts()
        {
            var searchText = SearchTextBox.Text;
            var category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            var filteredContacts = _mainController.ContactsController.SearchContacts(
                _mainController.CurrentUser.Id, searchText, category);

            ContactsDataGrid.ItemsSource = filteredContacts;
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            // Реализация добавления контакта
        }

        private void EditContactButton_Click(object sender, RoutedEventArgs e)
        {
            // Реализация редактирования контакта
        }

        private void DeleteContactButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContactsDataGrid.SelectedItem is Contact selectedContact)
            {
                if (MessageBox.Show("Удалить контакт?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _mainController.ContactsController.DeleteContact(selectedContact.Id);
                    LoadContacts();
                }
            }
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            // Реализация добавления события
        }

        private void CompleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventsDataGrid.SelectedItem is Event selectedEvent)
            {
                _mainController.EventsController.CompleteEvent(selectedEvent.Id);
                LoadEvents();
            }
        }
    }
}
