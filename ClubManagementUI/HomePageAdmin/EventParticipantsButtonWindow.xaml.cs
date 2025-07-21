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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClubManagement.DAL.Entities;
using ClubManagement.DLL.Services;
using Microsoft.Extensions.Logging;

namespace ClubManagementUI.HomePageAdmin
{
    /// <summary>
    /// Interaction logic for EventParticipantsButtonWindow.xaml
    /// </summary>
    public partial class EventParticipantsButtonWindow : UserControl
    {
        private EventParticipantService _eventParService = new();
        private EventParticipant? _eventParSelect = null;
        private bool _isRefreshingGrid = false;
        public EventParticipantsButtonWindow()
        {
            InitializeComponent();
        }

        private void FillDataGird()
        {
            _isRefreshingGrid = true;

            EventParDataGird.ItemsSource = null;
            EventParDataGird.ItemsSource = _eventParService.GetAllEventPar();

            _isRefreshingGrid = false;
        }
        private void FillComboBox()
        {
            StatusComBox.ItemsSource = new List<string> { "Registered", "Attended", "Absent" };
            StatusComBox.SelectedItem = "Attended";

            SearchStatusComBox.ItemsSource = new List<string> { "Registered", "Attended", "Absent" };
            SearchStatusComBox.SelectedItem = "";
        }
        private void FillElements(EventParticipant x)
        {
            if (x == null)
            {
                return;
            }
            EventParticipantIdTextBox.Text = x.EventParticipantId.ToString();
            EventParticipantIdTextBox.IsEnabled = false;

            UserNameTextBox.Text = x.User.FullName;
            UserNameTextBox.IsEnabled = false;
            EventNameTextBox.Text = x.Event.EventName;
            EventNameTextBox.IsEnabled = false;

            StatusComBox.SelectedItem = x.Status;
        }

        private void AddEventParButton_Click(object sender, RoutedEventArgs e)
        {
            EventParticipant newEventParticipant = new EventParticipant();

            newEventParticipant.EventId = _eventParSelect.EventId;
            newEventParticipant.UserId = _eventParSelect.UserId;
            newEventParticipant.Status = StatusComBox.SelectedItem.ToString() ?? "";

            newEventParticipant.EventParticipantId = _eventParSelect.EventParticipantId;
            _eventParService.UpdateEventPar(newEventParticipant);
            ClearForm();
            _eventParSelect = null;
            FillDataGird();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
            FillDataGird();
        }

        private void ClearForm()
        {
            EventParticipantIdTextBox.Text = "";
            UserNameTextBox.Text = "";
            EventNameTextBox.Text = "";
            StatusComBox.SelectedIndex = 1;
        }

        private void DeleteEventParButton_Click(object sender, RoutedEventArgs e)
        {
            EventParticipant? selected = EventParDataGird.SelectedItem as EventParticipant;
            if (selected == null)
            { //user ko chọn dòng nào mà lại nhấn xóa
                MessageBox.Show("Please select a row/ an event before delete", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult answer = MessageBox.Show("Do you really want to delete?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
            {
                return;
            }
            _eventParService.DeleteEventPar(selected);
            ClearForm();
            _eventParSelect = null;
            FillDataGird();
        }

        private void ResetEventParButton_Click(object sender, RoutedEventArgs e)
        {
            if (_eventParSelect == null)
            {
                ClearForm();
            }
            else
            {
                FillElements(_eventParSelect);
            }
        }

        private void EventParDataGird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isRefreshingGrid) return;
            EventParticipant? selected = EventParDataGird.SelectedItem as EventParticipant;
            if (selected == null)
            { //user ko chọn dòng nào mà lại nhấn edit
                MessageBox.Show("Please select a row/ an event before editing", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _eventParSelect = selected;
            FillElements(_eventParSelect);
        }

        private void SearchEventParButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameSearchTextBox.Text.Trim();
            string status = SearchStatusComBox.SelectedItem?.ToString() ?? "";

            var result = _eventParService.SearchByNameAndStatus(name, status);
            EventParDataGird.ItemsSource = null;
            EventParDataGird.ItemsSource = result;
        }
    }
}
