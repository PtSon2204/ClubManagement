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
        private User _currentUser;
        private UserService _userService = new();
        public UserButtonWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void FillDataGrid()
        {
            UserDataGird.ItemsSource = null;
            UserDataGird.ItemsSource = _userService.GetUserInClub(_currentUser.ClubId.Value);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
    }
}
