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
using ClubManagement.DAL;
using ClubManagement.DLL.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using Microsoft.Win32;
using ClubManagement.DAL.Entities;

namespace ClubManagementUI.HomePageAdmin
{
    /// <summary>
    /// Interaction logic for StatisticButtonWindow.xaml
    /// </summary>
    public partial class StatisticButtonWindow : UserControl
    {
        private ClubService _clubService = new();
        private ClubManagementContext _context;
        public StatisticButtonWindow()
        {
            InitializeComponent();
            _context = new();
        }
        private void FillComBox()
        {
            ClubCombox.ItemsSource = _clubService.GetAllClub();
            ClubCombox.DisplayMemberPath = "ClubName";
            ClubCombox.SelectedValuePath = "ClubId";

            ClubCombox2.ItemsSource = _clubService.GetAllClub();
            ClubCombox2.DisplayMemberPath = "ClubName";
            ClubCombox2.SelectedValuePath = "ClubId";
        }
        private void FillDataGrid()
        {
            MemberStatsGrid.ItemsSource = null;
            MemberStatsGrid.ItemsSource = _clubService.GetClubTotalMember();

            EventStatsGrid.ItemsSource= null;
            EventStatsGrid.ItemsSource = _clubService.GetClubTotalEvent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillComBox();
            FillDataGrid();
            TotalClub.Text = "Tổng số câu lạc bộ: " + $"{_context.Users.Count()}";
            TotalEvent.Text = "Tổng số sự kiện: " + $"{_context.Events.Count()}";
        }

        private void StatisticClub_Click(object sender, RoutedEventArgs e)
        {
            if (ClubCombox.SelectedValue != null)
            {
                int selectedClubId = (int)ClubCombox.SelectedValue;
                MemberStatsGrid.ItemsSource = null;
                MemberStatsGrid.ItemsSource = _clubService.GetClubStatisticMemberByClubId(selectedClubId);
            }
        }

        private void StatisticEvent_Click(object sender, RoutedEventArgs e)
        {
            if (ClubCombox.SelectedValue != null)
            {
                int selectedClubId = (int)ClubCombox.SelectedValue;
                EventStatsGrid.ItemsSource = null;
                EventStatsGrid.ItemsSource = _clubService.GetClubStatisticEventByClubId(selectedClubId);
            }
        }

        private void ExportClub_Click(object sender, RoutedEventArgs e)
        {
            var reports = MemberStatsGrid.ItemsSource as List<ClubStatisticDTO>;
            if (reports == null || reports.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Chọn nơi lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "UserReports.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Báo cáo");

                    // Header
                    worksheet.Cells[1, 1].Value = "ClubId";
                    worksheet.Cells[1, 2].Value = "ClubName";
                    worksheet.Cells[1, 3].Value = "Count";

                    // Dữ liệu
                    int row = 2;
                    foreach (var report in reports)
                    {
                        worksheet.Cells[row, 1].Value = report.ClubId;
                        worksheet.Cells[row, 2].Value = report.ClubName;
                        worksheet.Cells[row, 3].Value = report.Count;
                        row++;
                    }
                    worksheet.Cells[row, 2].Value = TotalClub.Text;
                    worksheet.Cells[row, 2, row, 3].Merge = true;
                    worksheet.Cells[row, 2].Style.Font.Bold = true;
                    worksheet.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Autofit
                    worksheet.Cells.AutoFitColumns();

                    // Lưu file
                    File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                    MessageBox.Show("Xuất file thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void ExportEvent_Click(object sender, RoutedEventArgs e)
        {
            var reports = MemberStatsGrid.ItemsSource as List<ClubStatisticDTO>;
            if (reports == null || reports.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Chọn nơi lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "EventReports.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Báo cáo");

                    // Header
                    worksheet.Cells[1, 1].Value = "ClubId";
                    worksheet.Cells[1, 2].Value = "ClubName";
                    worksheet.Cells[1, 3].Value = "Count";

                    // Dữ liệu
                    int row = 2;
                    foreach (var report in reports)
                    {
                        worksheet.Cells[row, 1].Value = report.ClubId;
                        worksheet.Cells[row, 2].Value = report.ClubName;
                        worksheet.Cells[row, 3].Value = report.Count;
                        row++;
                    }
                    worksheet.Cells[row, 2].Value = TotalEvent.Text;
                    worksheet.Cells[row, 2, row, 3].Merge = true;
                    worksheet.Cells[row, 2].Style.Font.Bold = true;
                    worksheet.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Autofit
                    worksheet.Cells.AutoFitColumns();

                    // Lưu file
                    File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                    MessageBox.Show("Xuất file thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
