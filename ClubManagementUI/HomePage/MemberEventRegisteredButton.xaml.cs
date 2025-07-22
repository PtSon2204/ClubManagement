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
    /// Interaction logic for MemberEventRegisteredButton.xaml
    /// </summary>
    public partial class MemberEventRegisteredButton : UserControl
    {
        private User account;
        private EventParticipantService _eventParticipantService = new();
        public MemberEventRegisteredButton(User account)
        {
            InitializeComponent();
            this.account = account;
        }
        private void FillDataGrid()
        {
            EventParDataGrid.ItemsSource = null;
            EventParDataGrid.ItemsSource = _eventParticipantService.GetAllEventParByClubIdUserId(account.ClubId.Value, account.UserId);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
    }
}
