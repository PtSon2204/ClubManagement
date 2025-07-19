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

namespace ClubManagementUI.HomeChairman
{
    /// <summary>
    /// Interaction logic for ViewReportButtonWindow.xaml
    /// </summary>
    public partial class ViewReportButtonWindow : UserControl
    {
        private User? getMember;
        private ReportService _reportService = new();
        public ViewReportButtonWindow(User? getMember)
        {
            InitializeComponent();
            this.getMember = getMember;
        }

        private void SearchReportButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? fromDate = SearchFromDate.SelectedDate;
            DateTime? toDate = SearchToDate.SelectedDate;

            if (fromDate == null || toDate == null)
            {
                MessageBox.Show("Vui lòng chọn cả Từ ngày và Đến ngày");
                return;
            }

            if (fromDate > toDate)
            {
                MessageBox.Show("Từ ngày không được sau Đến ngày");
                return;
            }

            var result = _reportService.SearchByDate(fromDate.Value, toDate.Value);
            ReportDataGird.ItemsSource = result;
        }

        private void FillDataGrid()
        {
            ReportDataGird.ItemsSource = null;
            ReportDataGird.ItemsSource = _reportService.GetReportsByClubId(getMember.ClubId.Value);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
    }
}
