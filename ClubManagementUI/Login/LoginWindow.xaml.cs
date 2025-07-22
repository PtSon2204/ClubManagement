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
using ClubManagement.DLL.Services;
using ClubManagementUI.HomeChairman;
using ClubManagementUI.HomePage;
using ClubManagementUI.HomePageAdmin;
using ClubManagementUI.HomePageVicechairman;
using ClubManagementUI.Log;

namespace ClubManagementUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserService _service = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBoxLogin.Text;
            string password = PasswordBoxLogin.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Both email and password are required!", "Fields required", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User? account = _service.GetAccount(email, password);
            if (account == null)
            {
                MessageBox.Show("Invalid email or password!", "Wrong credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (account.Role.ToLower())
            {
                case "admin":
                    var adminWindow = new HomeAdminWindow(account);
                    adminWindow.Show();
                    break;

                case "chairman":
                    var chairmanWindow = new HomeChairmanWindow(account);  // hoặc truyền luôn user nếu muốn
                    chairmanWindow.Show();
                    break;

                case "vicechairman":
                    var viceChairmanWindow = new HomeVicechairmanWindow(account);
                    viceChairmanWindow.Show();
                    break;

                case "member":
                    var memberWindow = new HomeWindow(account);
                    memberWindow.Show();
                    break;

                  default:
                    MessageBox.Show("Unknown role.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
            }

            this.Hide();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Do you really want to exit?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
            {
                return;
            }
            Application.Current.Shutdown();
        }

        private void ForgetPass_Click(object sender, RoutedEventArgs e)
        {
            var fogetpass = new ForgetPassWindow();
            fogetpass.Show();
        }
    }
}
