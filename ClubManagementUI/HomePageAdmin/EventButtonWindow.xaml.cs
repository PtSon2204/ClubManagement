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

namespace ClubManagementUI.HomePageAdmin
{
    /// <summary>
    /// Interaction logic for EventButtonWindow.xaml
    /// </summary>
    public partial class EventButtonWindow : UserControl
    {
        private EventService _eventService = new();
        private ClubService _clubService = new();
        private Event? _eventSelect = null;
        private bool _isRefreshingGrid = false;
        public EventButtonWindow()
        {
            InitializeComponent();
        }
        private void FillDataGird()
        {
            _isRefreshingGrid = true;

            EventDataGird.ItemsSource = null;
            EventDataGird.ItemsSource = _eventService.GetAllEvent();

            _isRefreshingGrid = false;
        }

        private void FillComboBox()
        {
            ClubIdComBox.ItemsSource = _clubService.GetAllClub();
            ClubIdComBox.DisplayMemberPath = "ClubName"; //hiển thị cột nào
            ClubIdComBox.SelectedValuePath = "ClubId"; //lấy id để ném làm fk của user
            ClubIdComBox.SelectedValue = 1;
        }
        private void FillElements(Event x)
        {
            if (x == null)
            {
                return;
            }
            EventIdTextBox.Text = x.EventId.ToString();
            EventIdTextBox.IsEnabled = false;

            EventNameTextBox.Text = x.EventName;
            DescriptionTextBox.Text = x.Description;
            LocationTextBox.Text = x.Location;
            EventDateDatePic.Text = x.EventDate.ToString();
            ClubIdComBox.SelectedValue = x.ClubId;
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            Event newEvent = new Event();

            newEvent.EventName = EventNameTextBox.Text; 
            newEvent.Description = DescriptionTextBox.Text;
            newEvent.Location = LocationTextBox.Text;
            if (EventDateDatePic.SelectedDate.HasValue)
            {
                newEvent.EventDate = EventDateDatePic.SelectedDate.Value;
            }
            newEvent.ClubId = (int)ClubIdComBox.SelectedValue;

            if (_eventSelect is null)
            {
                _eventService.AddEvent(newEvent);
            }
            else
            {
                newEvent.EventId = _eventSelect.EventId;
                _eventService.UpdateEvent(newEvent);
            }
            ClearForm();
            _eventSelect = null;
            FillDataGird();
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            Event? selected = EventDataGird.SelectedItem as Event;
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
            _eventService.DeleteEvent(selected);
            ClearForm();
            _eventSelect = null;
            FillDataGird();
        }

        private void ResetEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (_eventSelect == null)
            {
                ClearForm();
            }
            else
            {
                FillElements(_eventSelect);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
            FillDataGird();
        }

        private void SearchEventButton_Click(object sender, RoutedEventArgs e)
        {
            string name = EventNameSearchTextBox.Text;
            string date = EventDateSearchDatePic.SelectedDate?.ToString("yyyy-MM-dd") ?? "";

            var result = _clubService.SearchClubByNameOrEstabDate(name, date);
            EventDataGird.ItemsSource = null;
            EventDataGird.ItemsSource = result;
        }

        private void EventDataGird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isRefreshingGrid) return;
            Event? selected = EventDataGird.SelectedItem as Event;
            if (selected == null)
            { //user ko chọn dòng nào mà lại nhấn edit
                MessageBox.Show("Please select a row/ an event before editing", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _eventSelect = selected;
            FillElements(_eventSelect);
        }

        private void ClearForm()
        {
            EventIdTextBox.Text = "";
            EventNameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            LocationTextBox.Text = "";
            EventDateDatePic.SelectedDate = null;
            ClubIdComBox.SelectedIndex = 1;
        }
    }
}
