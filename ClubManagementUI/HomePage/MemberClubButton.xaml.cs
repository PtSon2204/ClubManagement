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
    /// Interaction logic for MemberClubButton.xaml
    /// </summary>
    public partial class MemberClubButton : UserControl
    {
        private User account;
        private ClubService _clubService = new();

        public MemberClubButton(User account)
        {
            InitializeComponent();
            this.account = account;
        }

        private void FillDataGrid()
        {
            ClubDataGrid.ItemsSource = null;
            ClubDataGrid.ItemsSource = _clubService.GetAllClub();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
