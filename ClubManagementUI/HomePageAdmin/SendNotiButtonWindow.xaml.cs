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
using Microsoft.IdentityModel.Tokens;

namespace ClubManagementUI.HomePageAdmin
{
    /// <summary>
    /// Interaction logic for SendNotiButtonWindow.xaml
    /// </summary>
    public partial class SendNotiButtonWindow : UserControl
    {
        public SendNotiButtonWindow()
        {
            InitializeComponent();
        }

        private void SendNotica_Click(object sender, RoutedEventArgs e)
        {
            string to = EmailSendTextBox.Text.Trim();
            string content = ContentSendTextBox.Text.Trim();
            string subject = "Thông báo từ hệ thống CLB"; // hoặc cho người nhập

            if (string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ email và nội dung!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool success = Validation.Validate.SendEmail(to, subject, content);

            if (success)
            {
                MessageBox.Show("Gửi email thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ClearForm()
        {
            ContentSendTextBox.Text = "";
            EmailSendTextBox.Text = "";
        }

        private void ResetNotica_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }
    }
}
