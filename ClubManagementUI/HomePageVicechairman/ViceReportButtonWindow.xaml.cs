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

namespace ClubManagementUI.HomePageVicechairman
{
    /// <summary>
    /// Interaction logic for ViceReportButtonWindow.xaml
    /// </summary>
    public partial class ViceReportButtonWindow : UserControl
    {
        private User? getMember;
        private ReportService _reportService = new();
        private ClubService _clubService = new();
        private Report? _reportSelect = null;
        private bool _isRefreshingGrid = false;
        public ViceReportButtonWindow(User? getMember)
        {
            InitializeComponent();
            this.getMember = getMember;
        }

        private void SaveReportButton_Click(object sender, RoutedEventArgs e)
        {
            Report newReport = new Report();

            newReport.Semester = SemesterTextBox.Text; ;
            newReport.ParticipationStats = ParticipationStatusTextBox.Text;
            newReport.EventSummary = EventSummaryTextBox.Text;
            newReport.MemberChanges = MemberChangesTextBox.Text;
            newReport.CreatedDate = CreatedDateDatePic.SelectedDate.Value;
            newReport.ClubId = getMember.ClubId;

            if (_reportSelect is null)
            {
                _reportService.AddReport(newReport);
            }
            else
            {
                newReport.ReportId = _reportSelect.ReportId;
                _reportService.UpdateReport(newReport);
            }
            ClearForm();
            _reportSelect = null;
            FillDataGrid();

        }

        private void DeleteReportButton_Click(object sender, RoutedEventArgs e)
        {
            Report? selected = ReportDataGird.SelectedItem as Report;
            if (selected == null)
            { //user ko chọn dòng nào mà lại nhấn xóa
                MessageBox.Show("Please select a row/ an report before delete", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult answer = MessageBox.Show("Do you really want to delete?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
            {
                return;
            }
            _reportService.DeleteReport(selected);
            ClearForm();
            _reportSelect = null;
            FillDataGrid();

        }

        private void ResetReportButton_Click(object sender, RoutedEventArgs e)
        {
            if (_reportSelect == null)
            {
                ClearForm();
            }
            else
            {
                FillElements(_reportSelect);
            }

        }
        private void ClearForm()
        {
            ReportIdTextBox.Text = "";
            SemesterTextBox.Text = "";
            EventSummaryTextBox.Text = "";
            MemberChangesTextBox.Text = "";
            ParticipationStatusTextBox.Text = "";
            CreatedDateDatePic.Text = "";
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

        private void FillElements(Report x)
        {
            if (x == null)
            {
                return;
            }
            ReportIdTextBox.Text = x.ReportId.ToString();
            ReportIdTextBox.IsEnabled = false;

            SemesterTextBox.Text = x.Semester;
            MemberChangesTextBox.Text = x.MemberChanges;
            EventSummaryTextBox.Text = x.EventSummary;
            ParticipationStatusTextBox.Text = x.ParticipationStats;
            CreatedDateDatePic.Text = x.CreatedDate.ToString();
        }


        private void FillDataGrid()
        {
            _isRefreshingGrid = true;

            ReportDataGird.ItemsSource = null;
            ReportDataGird.ItemsSource = _reportService.GetReportsByClubId(getMember.ClubId.Value);

            _isRefreshingGrid = false;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void ReportDataGird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isRefreshingGrid) return;
            Report? selected = ReportDataGird.SelectedItem as Report;
            if (selected == null)
            { //user ko chọn dòng nào mà lại nhấn edit
                MessageBox.Show("Please select a row/ an report before editing", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _reportSelect = selected;
            FillElements(_reportSelect);
        }
    }
}
