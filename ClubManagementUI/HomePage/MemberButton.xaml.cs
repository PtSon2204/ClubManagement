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
using ClubManagement.DLL.Services;

namespace ClubManagementUI.HomePage
{

    public partial class MemberButton : UserControl
    {
        private ClubService _clubService = new();

        public MemberButton()
        {
            InitializeComponent();
            LoadClubList();
        }

        private void LoadClubList()
        {
            var clubs = _clubService.GetAllClub();
            ClubDataGrid.ItemsSource = clubs;
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is int clubId)
            {
                // TODO: Lấy UserID hiện tại từ session hoặc đăng nhập
                int currentUserId = 1; // Tạm hardcode để test

                // TODO: Thêm User vào Club, ví dụ tạo ClubUser hoặc Update User.ClubID
                MessageBox.Show($"Bạn đã tham gia câu lạc bộ ID: {clubId}");
            }
        }
    }
}
