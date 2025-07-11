using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for UserButtonWindow.xaml
    /// </summary>
    public partial class UserButtonWindow : UserControl
    {
        private UserService _userService = new();
        private ClubService _clubService = new();
        private User? _userSelect = null;
        private bool _isRefreshingGrid = false;

        public UserButtonWindow()
        {
            InitializeComponent();
        }

        //đổ data vào lưới
        private void FillDataGird()
        {
            _isRefreshingGrid = true;

            UserDataGird.ItemsSource = null;
            UserDataGird.ItemsSource = _userService.GetAllUser();

            _isRefreshingGrid = false;
        }

        //hàm chọn 1 data để xóa hoặc sửa trên cái grid
        private void UserDataGird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isRefreshingGrid) return;
            User? selected = UserDataGird.SelectedItem as User;
            if (selected == null)
            { //user ko chọn dòng nào mà lại nhấn edit
                MessageBox.Show("Please select a row/ an air con before editing", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _userSelect = selected;
            FillElements(_userSelect);
        }

        //hàm này khi chạy nó sẽ loaded dữ liệu vào lưới/grid
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
            FillDataGird();
        }

        //hàm này đổ data vào combo box
        private void FillComboBox()
        {
            ClubIdComBox.ItemsSource = _clubService.GetAllClub();
            ClubIdComBox.DisplayMemberPath = "ClubName"; //hiển thị cột nào
            ClubIdComBox.SelectedValuePath = "ClubId"; //lấy id để ném làm fk của user
            UserRoleComBox.ItemsSource = new List<string> { "Admin", "Chairman", "ViceChairman", "TeamLeader", "Member" };
        }
        
        //hàm này đổ dữ liệu vào text khi chọn 1 dòng để cập nhật
        private void FillElements(User user)
        {
            if (user == null)
            {
                return;
            }
            UserIdTextBox.Text = user.UserId.ToString();
            UserIdTextBox.IsEnabled = false;

            UserNameTextBox.Text = user.FullName;
            UserEmailTextBox.Text = user.Email;  
            UserPasswordTextBox.Text = user.Password;
            UserRoleComBox.SelectedItem = user.Role;
            ClubIdComBox.SelectedValue = user.ClubId;
        }

        //hàm này xử lí thêm hoặc cập nhật
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            string? selectedRole = UserRoleComBox.SelectedItem?.ToString();
            // Gọi validate
            if (!Validation.Validate.GetFormatAddUser_SON(UserNameTextBox.Text, UserPasswordTextBox.Text, selectedRole, UserEmailTextBox.Text, ClubIdComBox.SelectedValue))
                return;

            User newUser = new User();

            newUser.FullName = UserNameTextBox.Text; ;
            newUser.Role = selectedRole;
            newUser.Email = UserEmailTextBox.Text;
            newUser.Password = UserPasswordTextBox.Text;
            newUser.ClubId = (int)ClubIdComBox.SelectedValue;

            if (_userSelect is null)
            {
                _userService.AddUser(newUser);
            } else
            {
                newUser.UserId = _userSelect.UserId;
                _userService.UpdateUser(newUser);
            }
            ClearForm();
            _userSelect = null;
            FillDataGird();
        }

        //hàm này sẽ xóa khi không chọn dòng và khi cập nhật xong thì các textbox sẽ rỗng
        private void ClearForm()
        {
            UserIdTextBox.Text = "";
            UserNameTextBox.Text = "";
            UserEmailTextBox.Text = "";
            UserPasswordTextBox.Text = "";
            UserRoleComBox.SelectedItem = null;
            ClubIdComBox.SelectedIndex = -1;
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            User? selected = UserDataGird.SelectedItem as User;
            if (selected == null)
            { //user ko chọn dòng nào mà lại nhấn xóa
                MessageBox.Show("Please select a row/ an air con before delete", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            FillDataGird();
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
    }
}
