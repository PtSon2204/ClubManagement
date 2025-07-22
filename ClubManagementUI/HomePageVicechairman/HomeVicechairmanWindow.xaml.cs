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

namespace ClubManagementUI.HomePageVicechairman
{
    /// <summary>
    /// Interaction logic for HomeVicechairmanWindow.xaml
    /// </summary>
    public partial class HomeVicechairmanWindow : Window
    {
        private User account;

        public HomeVicechairmanWindow(User account)
        {
            InitializeComponent();
            this.account = account;
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

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViceReportButtonWindow(account);
        }

        private void AttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViceAcceptMemberWindow(account);
        }

        private void ChangePassButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViceChangePassWindow(account);
        }
    }
}
