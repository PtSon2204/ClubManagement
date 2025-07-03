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
using ClubManagement.DAL.Entities;

namespace ClubManagementUI.HomePageAdmin
{
    /// <summary>
    /// Interaction logic for HomeAdminWindow.xaml
    /// </summary>
    public partial class HomeAdminWindow : Window
    {
        public User? GetMember { get; set; } = null;
        public HomeAdminWindow()
        {
            InitializeComponent();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to log out?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var login = new LoginWindow();
                login.Show();
                this.Close();
            }
        }
    }
}
