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
using ClubManagement.DAL;
using ClubManagement.DAL.Entities;
using ClubManagement.DLL.Services;

namespace ClubManagementUI.HomePageVicechairman
{
    /// <summary>
    /// Interaction logic for ViceAcceptMemberWindow.xaml
    /// </summary>
    public partial class ViceAcceptMemberWindow : UserControl
    {
        private User? getMember;
        private EventParticipantService _eventParService = new();
        private EventParticipant? _eventParSelect = null;
        private UserService _userService = new();
        private bool _isRefreshingGrid = false;

        public ViceAcceptMemberWindow(User? getMember)
        {
            InitializeComponent();
            this.getMember = getMember;
        }
        private void FillStatus(EventParticipant x)
        {
            StatusCombox.SelectedValue = x.Status;
        }
        private void FillDataGrid()
        {
            _isRefreshingGrid = true;

            EventParDataGird.ItemsSource = null;
            EventParDataGird.ItemsSource = _eventParService.GetAllEventParByClubId(getMember.ClubId.Value);

            _isRefreshingGrid = false;
        }

        private void FillCombox()
        {
            StatusCombox.ItemsSource = null;
            StatusCombox.ItemsSource = _eventParService.GetAllEventStatus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillCombox();
            FillDataGrid();
        }
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            EventParticipant? selected = EventParDataGird.SelectedItem as EventParticipant;
            if (selected == null)
            { //user ko chọn dòng nào mà lại nhấn xóa
                MessageBox.Show("Please select a row/ an user before kick", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult answer = MessageBox.Show("Do you really want to kick?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
            {
                return;
            }
            _eventParService.DeleteEventPar(selected);
            ClearForm();
            FillDataGrid();
        }

        private void SearchUserButton_Click(object sender, RoutedEventArgs e)
        {
            var result = _userService.SearchUserByNameOrEmail(NameSearchTextBox.Text, EmailSearchTextBox.Text);
            EventParDataGird.ItemsSource = null;
            EventParDataGird.ItemsSource = result;
        }

        private void SaveUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (_eventParSelect == null)
            {
                MessageBox.Show("Please select a user to update status.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string? newStatus = StatusCombox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Please select a status.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Lấy sự kiện từ EventParticipant
            var eventInfo = _eventParSelect.Event;
            if (eventInfo == null)
            {
                MessageBox.Show("Event information is missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime today = DateTime.Today;
            DateTime eventDate = eventInfo.EventDate.Date; // So sánh theo ngày

            // Trường hợp chưa tới ngày sự kiện
            if (today < eventDate)
            {
                if (newStatus == "Attend" || newStatus == "Absent")
                {
                    MessageBox.Show("You can only mark Attend/Absent on the event date!", "Invalid Status", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            // Trường hợp sau ngày sự kiện
            if (today > eventDate)
            {
                MessageBox.Show("You cannot change status after the event date.", "Too Late", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Cập nhật nếu hợp lệ
            _eventParSelect.Status = newStatus;
            _eventParService.UpdateEventPar(_eventParSelect);
            ClearForm();
            _eventParSelect = null;
            FillDataGrid();
        }

        private void EventParDataGird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isRefreshingGrid) return;
            EventParticipant? selected = EventParDataGird.SelectedItem as EventParticipant;
            if (selected == null)
            { 
                MessageBox.Show("Please select a row/ an user before update status", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _eventParSelect = selected;
            FillStatus(selected);
        }

        private void ClearForm()
        {
            StatusCombox.SelectedItem = null;
        }
    }
}
