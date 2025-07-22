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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using Microsoft.Win32;

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

        private void ExportEvent_Click(object sender, RoutedEventArgs e)
        {
            var reports = ReportDataGird.ItemsSource as List<Report>;
            if (reports == null || reports.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Chọn nơi lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "ClubReports.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Báo cáo");

                    // Header
                    worksheet.Cells[1, 1].Value = "ReportId";
                    worksheet.Cells[1, 2].Value = "Semester";
                    worksheet.Cells[1, 3].Value = "MemberChanges";
                    worksheet.Cells[1, 4].Value = "EventSummary";
                    worksheet.Cells[1, 5].Value = "ParticipationStats";
                    worksheet.Cells[1, 6].Value = "CreatedDate";

                    // Dữ liệu
                    int row = 2;
                    foreach (var report in reports)
                    {
                        worksheet.Cells[row, 1].Value = report.ReportId;
                        worksheet.Cells[row, 2].Value = report.Semester;
                        worksheet.Cells[row, 3].Value = report.MemberChanges;
                        worksheet.Cells[row, 4].Value = report.EventSummary;
                        worksheet.Cells[row, 5].Value = report.ParticipationStats;
                        worksheet.Cells[row, 6].Value = report.CreatedDate.Value.ToString("dd/MM/yyyy");
                        row++;
                    }

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
