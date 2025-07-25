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
    /// Interaction logic for UserButtonWindow.xaml
    /// </summary>
    public partial class UserButtonWindow : UserControl
    {
        private UserService _userService = new();
        private User _userSelect = null;
        private bool _isRefreshingGrid = false;
        private User? getMember;

        public UserButtonWindow(User? getMember)
        {
            InitializeComponent();
            this.getMember = getMember;
        }

        private void FillDataGrid()
        {
            _isRefreshingGrid = true;

            UserDataGird.ItemsSource = null;
            UserDataGird.ItemsSource = _userService.GetUserInClub(getMember.UserId);

            _isRefreshingGrid = false;
        }
        private void FillComboBox()
        {
            UserRoleComBox.ItemsSource = new List<string> { "Chairman", "ViceChairman", "TeamLeader", "Member" };
            UserRoleComBox.SelectedItem = "Member";
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
            FillDataGrid();
        }
        private void FillElements(User user)
        {
            if (user == null)
            {
                return;
            }
            UserIdTextBox.Text = user.UserId.ToString();
            UserIdTextBox.IsEnabled = false;

            UserNameTextBox.Text = user.FullName;
            UserNameTextBox.IsEnabled = false;

            UserEmailTextBox.Text = user.Email;
            UserEmailTextBox.IsEnabled = false;

            UserRoleComBox.SelectedItem = user.Role;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedRole = UserRoleComBox.SelectedItem?.ToString();
            int clubId = getMember.ClubId.Value;
            if (selectedRole == "Chairman" || selectedRole == "ViceChairman")
            {
                int? currentUserId = _userSelect?.UserId;

                if (!Validation.Validate.IsChairmanOrViceExists(clubId, selectedRole, currentUserId))
                {
                    MessageBox.Show("This club already has a Chairman or ViceChairman. You cannot assign more than one.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            if (_userSelect != null)
            {
                _userSelect.FullName = UserNameTextBox.Text;
                _userSelect.Role = UserRoleComBox.SelectedItem?.ToString();
                _userSelect.Email = UserEmailTextBox.Text;

                _userService.UpdateUser(_userSelect); // Update đối tượng đã được theo dõi (tracked)
            }

            ClearForm();
            _userSelect = null;
            FillDataGrid();
        }
        
        private void ClearForm()
        {
            UserIdTextBox.Text = "";
            UserNameTextBox.Text = "";
            UserEmailTextBox.Text = "";
            UserRoleComBox.SelectedItem = null;
        }
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            User? selected = UserDataGird.SelectedItem as User;
            if (selected == null)
            { //user ko chọn dòng nào mà lại nhấn xóa
                MessageBox.Show("Please select a row/ an user before delete", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult answer = MessageBox.Show("Do you really want to delete?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
            {
                return;
            }
            _userService.DeleteUser(selected);
            ClearForm();
            _userSelect = null;
            FillDataGrid();
        }

        private void ResetUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (_userSelect == null)
            {
                ClearForm();
            }
            else
            {
                FillElements(_userSelect);
            }
        }

        private void UserDataGird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isRefreshingGrid) return;
            User? selected = UserDataGird.SelectedItem as User;
            if (selected == null)
            { //user ko chọn dòng nào mà lại nhấn edit
                MessageBox.Show("Please select a row/ an user before editing", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _userSelect = selected;
            FillElements(_userSelect);
        }

        private void SearchUserButton_Click(object sender, RoutedEventArgs e)
        {
            var result = _userService.SearchUserByNameOrEmail(NameSearchTextBox.Text, EmailSearchTextBox.Text);
            UserDataGird.ItemsSource = null;
            UserDataGird.ItemsSource = result;
        }
    }
}
