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
using Microsoft.IdentityModel.Tokens;

namespace ClubManagementUI.HomePageVicechairman
{
    /// <summary>
    /// Interaction logic for ViceChangePassWindow.xaml
    /// </summary>
    public partial class ViceChangePassWindow : UserControl
    {
        private User? getMember;
        private UserService _userService = new();

        public ViceChangePassWindow(User? getMember)
        {
            InitializeComponent();
            this.getMember = getMember;
        }

        private void SavePassButton_Click(object sender, RoutedEventArgs e)
        {
            if (OldPassTextBox.Text.IsNullOrEmpty() || NewPassTextBox.Text.IsNullOrEmpty() || CfPassTextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter full!!!");
                return;
            }
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(OldPassTextBox.Text, getMember.Password);
            if (!isPasswordCorrect)
            {
                MessageBox.Show("OldPassword is incorrect. Please enter again!");
                return;
            }
            string newPass = NewPassTextBox.Text;
            string cfPass = CfPassTextBox.Text;
            if (newPass != cfPass)
            {
                MessageBox.Show("New password and confirm password must be the same.");
                return;
            }
            getMember.Password = newPass;
            _userService.UpdateUser(getMember);
            ClearForm();
        }

        private void ResetPassButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UserEmailTextBox.Text = getMember.Email;
        }

        private void ClearForm()
        {
            OldPassTextBox.Text = "";
            NewPassTextBox.Text = "";
            CfPassTextBox.Text = "";
        }
    }
}
