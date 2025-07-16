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
using ClubManagement.DAL.Repositories;
using ClubManagement.DLL.Services;

namespace ClubManagementUI.Log
{
    /// <summary>
    /// Interaction logic for ForgetPassWindow.xaml
    /// </summary>
    public partial class ForgetPassWindow : Window
    {
        private UserRepository _userRepo = new();
        public ForgetPassWindow()
        {
            InitializeComponent();
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SendMailButton_Click(object sender, RoutedEventArgs e)
        {
            var user = _userRepo.GetAccountRepo(EmailTextBox.Text);
            if (user == null)
            {
                MessageBox.Show("Email không tồn tại.");
                return;
            }

            string newPassword = Validation.Validate.GenerateRandomPassword();
            string hashed = BCrypt.Net.BCrypt.HashPassword(newPassword);

            user.Password = hashed;
            _userRepo.Update(user);

            string subject = "Khôi phục mật khẩu";
            string body = $"Mật khẩu mới của bạn là: {newPassword}\nVui lòng đổi mật khẩu sau khi đăng nhập.";
            Validation.Validate.SendEmail(EmailTextBox.Text, subject, body);

            MessageBox.Show("Mật khẩu mới đã được gửi đến email.");
        }

        private void ExitMailButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var login = new LoginWindow();
                login.Show();
                this.Close();
            }
        }
    }
}
