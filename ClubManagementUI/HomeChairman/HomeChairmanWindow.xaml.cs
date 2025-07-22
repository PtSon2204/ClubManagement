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

namespace ClubManagementUI.HomeChairman
{
    /// <summary>
    /// Interaction logic for HomeChairmanWindow.xaml
    /// </summary>
    public partial class HomeChairmanWindow : Window
    {
        private User account;
        public HomeChairmanWindow(User account)
        {
            InitializeComponent();
            this.account = account;
            string[] name = account.FullName.Split(" ");
            TitleTextBox.Text = "Welcome " + $"{name[name.Length - 1]}";
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

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserButtonWindow(account);
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ChairEventButtonWindow(account);
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ChairChangePassWindow(account);
        }

        private void ViewReport_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewReportButtonWindow(account);
        }
    }
}
