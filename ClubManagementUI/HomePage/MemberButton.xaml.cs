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
using ClubManagement.DAL.Repositories;
using ClubManagement.DLL.Services;

namespace ClubManagementUI.HomePage
{

    public partial class MemberButton : UserControl
    {
        private EventParticipantService _eventParticipantService = new();
        private EventService _eventService = new();
        private User account;
        public MemberButton(User account)
        {
            InitializeComponent();
            this.account = account;
        }

        private void FillDataGrid()
        {
            EventDataGrid.ItemsSource = null;
            EventDataGrid.ItemsSource = _eventService.GetAllEventByClubId(account.ClubId.Value);
        }
        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                Console.WriteLine($"Tag value: {button?.Tag}, getMember: {account}");

                if (button?.Tag is int eventId && account != null)
                {
                    Console.WriteLine($"EventId: {eventId}, UserId: {account.UserId}, ClubId: {account.ClubId}");

                    var existed = _eventParticipantService.GetAllEventPar()
                        .Any(x => x.EventId == eventId && x.UserId == account.UserId);

                    if (existed)
                    {
                        MessageBox.Show("Bạn đã đăng ký sự kiện này rồi!");
                        return;
                    }

                    EventParticipant participant = new EventParticipant
                    {
                        UserId = account.UserId,
                        EventId = eventId,
                        Status = "Pending"
                    };
                    _eventParticipantService.AddEventPar(participant);

                    MessageBox.Show("Đăng ký sự kiện thành công!");
                    FillDataGrid();
                }
                else
                {
                    MessageBox.Show("Lỗi: Không thể lấy thông tin sự kiện hoặc thành viên! " +
                        $"Tag is int: {button?.Tag is int}, getMember is null: {account == null}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng ký sự kiện: {ex.Message}");
                Console.WriteLine(ex.ToString());
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
    }
}