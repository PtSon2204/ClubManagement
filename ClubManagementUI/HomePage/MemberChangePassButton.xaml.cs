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

namespace ClubManagementUI.HomePage
{
    /// <summary>
    /// Interaction logic for MemberChangePassButton.xaml
    /// </summary>
    public partial class MemberChangePassButton : UserControl
    {
        private User account;
        private UserService _userService = new();

        public MemberChangePassButton(User account)
        {
            InitializeComponent();
            this.account = account;
        }

        private void SavePassButton_Click(object sender, RoutedEventArgs e)
        {
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(OldPassTextBox.Text, account.Password);
            if (!isPasswordCorrect)
            {
                System.Windows.MessageBox.Show("OldPassword is incorrect. Please enter again!");
                return;
            }
            string newPass = NewPassTextBox.Text;
            string cfPass = CfPassTextBox.Text;
            if (newPass != cfPass)
            {
                System.Windows.MessageBox.Show("New password and confirm password must be the same.");
                return;
            }
            account.Password = newPass;
            _userService.UpdateUser(account);
            ClearForm();
        }

        private void ResetPassButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            OldPassTextBox.Text = "";
            NewPassTextBox.Text = "";
            CfPassTextBox.Text = "";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UserEmailTextBox.Text = account.Email;
        }
    }
}
