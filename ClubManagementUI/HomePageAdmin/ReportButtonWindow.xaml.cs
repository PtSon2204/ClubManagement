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

namespace ClubManagementUI.HomePageAdmin
{
    /// <summary>
    /// Interaction logic for ReportButtonWindow.xaml
    /// </summary>
    public partial class ReportButtonWindow : UserControl
    {
        private ReportService _reportService = new();
        private Report? _reportSelect = null;
        private bool _isRefreshingGrid = false;
        public ReportButtonWindow()
        {
            InitializeComponent();
        }

        private void FillDataGird()
        {
            _isRefreshingGrid = true;

            ReportDataGird.ItemsSource = null;
            ReportDataGird.ItemsSource = _reportService.GetAllReport();

            _isRefreshingGrid = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGird();
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
            _reportSelect = null;
            FillDataGird();
        }
    }
}
