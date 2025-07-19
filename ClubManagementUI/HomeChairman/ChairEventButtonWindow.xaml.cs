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

namespace ClubManagementUI.HomeChairman
{
    /// <summary>
    /// Interaction logic for ChairEventButtonWindow.xaml
    /// </summary>
    public partial class ChairEventButtonWindow : UserControl
    {
        private User? getMember;
        private EventService _eventService = new();
        private bool _isRefreshingGrid = false;
        private Event? _eventSelect = null;
        private ClubService _clubService = new();

        public ChairEventButtonWindow(User? getMember)
        {
            InitializeComponent();
            this.getMember = getMember;
        }

        private void FillDataGrid()
        {
            _isRefreshingGrid = true;

            EventDataGird.ItemsSource = null;
            EventDataGird.ItemsSource = _eventService.GetAllEventByClubId(getMember.ClubId.Value);

            _isRefreshingGrid = false;
        }
        private void FillElements(Event x)
        {
            if (x == null)
            {
                return;
            }
            EventNameTextBox.Text = x.EventName;
            DescriptionTextBox.Text = x.Description;
            LocationTextBox.Text = x.Location;
            dateTimePicker.Text = x.EventDate.ToString();
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            Event newEvent = new Event();

            newEvent.EventName = EventNameTextBox.Text;
            newEvent.Description = DescriptionTextBox.Text;
            newEvent.Location = LocationTextBox.Text;
            if (dateTimePicker.Value.HasValue)
            {
                newEvent.EventDate = dateTimePicker.Value.Value;
            }
            newEvent.ClubId = getMember.ClubId;

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
            FillDataGrid();
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            Event? selected = EventDataGird.SelectedItem as Event;
            if (selected == null)
            { 
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
            FillDataGrid();
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

        private void EventDataGird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isRefreshingGrid) return;
            Event? selected = EventDataGird.SelectedItem as Event;
            if (selected == null)
            { 
                MessageBox.Show("Please select a row/ an event before editing", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _eventSelect = selected;
            FillElements(_eventSelect);
        }

        private void SearchEventButton_Click(object sender, RoutedEventArgs e)
        {
            string name = EventNameSearchTextBox.Text;
            string date = EventDateSearchDatePic.SelectedDate?.ToString("yyyy-MM-dd") ?? "";

            var result = _clubService.SearchClubByNameOrEstabDate(name, date);
            EventDataGird.ItemsSource = null;
            EventDataGird.ItemsSource = result;
        }
        private void ClearForm()
        {
            EventNameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            LocationTextBox.Text = "";
            dateTimePicker.Value = null;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
    }
}
