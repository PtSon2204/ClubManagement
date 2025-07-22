using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ClubManagementUI.HomePage
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>

    public partial class HomeWindow : Window
    {
        private User account;
        public HomeWindow(User account)
        {
            InitializeComponent();
            this.account = account;
            string[] name = account.FullName.Split(" ");
            TitleTextBox.Text = "Welcome " + $"{name[name.Length - 1]}";
        }
        
        //hàm logout
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
        private void MemberButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new MemberButton(account);
        }

        private void MemberButton_Click_2(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new MemberChangePassButton(account);
        }
    }
}