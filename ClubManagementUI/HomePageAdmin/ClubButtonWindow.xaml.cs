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
using Microsoft.IdentityModel.Tokens;

namespace ClubManagementUI.HomePageAdmin
{
    /// <summary>
    /// Interaction logic for ClubButtonWindow.xaml
    /// </summary>
    public partial class ClubButtonWindow : UserControl
    {
        private ClubService _clubService = new();
        private Club? _clubSelect = null;
        private bool _isRefreshingGrid = false;
        public ClubButtonWindow()
        {
            InitializeComponent();
        }

        private void FillDataGrid()
        {
            _isRefreshingGrid = true;

            ClubDataGird.ItemsSource = null;
            ClubDataGird.ItemsSource = _clubService.GetAllClub();

            _isRefreshingGrid = false;
        }

        private void FillElements(Club x)
        {
            if (x == null) return;

            ClubIdTextBox.Text = x.ClubId.ToString();
            ClubNameTextBox.Text = x.ClubName;
            DescriptionTextBox.Text = x.Description;
            EstablishedDate.Text = x.EstablishedDate.ToString();
        }

        private void AddClubButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClubNameTextBox.Text.IsNullOrEmpty() || DescriptionTextBox.Text.IsNullOrEmpty() || !EstablishedDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please enter full!!!");
                return;
            }

            Club newClub = new Club();
            newClub.ClubName = ClubNameTextBox.Text;
            newClub.Description = DescriptionTextBox.Text;
            if (EstablishedDate.SelectedDate.HasValue)
            {
                newClub.EstablishedDate = DateOnly.FromDateTime(EstablishedDate.SelectedDate.Value);
            }
            if (_clubSelect is null )
            {
                _clubService.AddClub(newClub);
            }
            else
            {
                newClub.ClubId = _clubSelect.ClubId;
                _clubService.UpdateClub(newClub);
            }
            ClearForm();
            _clubSelect = null;
            FillDataGrid();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void ClearForm()
        {
            ClubIdTextBox.Text = "";
            ClubNameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            EstablishedDate.SelectedDate = null;
        }

        private void ClubDataGird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isRefreshingGrid) return;
            Club? selected = ClubDataGird.SelectedItem as Club;
            if (selected == null)
            { 
                MessageBox.Show("Please select a row/ an club before editing", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _clubSelect = selected;
            FillElements(_clubSelect);
        }

        private void DeleteClubButton_Click(object sender, RoutedEventArgs e)
        {
            Club? selected = ClubDataGird.SelectedItem as Club;
            if (selected == null)
            { 
                MessageBox.Show("Please select a row/ an club before delete", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult answer = MessageBox.Show("Do you really want to delete?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
            {
                return;
            }
            _clubService.DeleteClub(selected);
            ClearForm();
            _clubSelect = null;
            FillDataGrid();
        }

        private void ResetClubButton_Click(object sender, RoutedEventArgs e)
        {
            if (_clubSelect == null)
            {
                ClearForm();
            }
            else
            {
                FillElements(_clubSelect);
            }
        }

        private void SearchClubButton_Click(object sender, RoutedEventArgs e)
        {
            // Gọi hàm từ UI
            string name = ClbNameSearchTextBox.Text;
            string date = EstaDateSearchTextBox.SelectedDate?.ToString("yyyy-MM-dd") ?? "";

            var result = _clubService.SearchClubByNameOrEstabDate(name, date);
            ClubDataGird.ItemsSource = null;
            ClubDataGird.ItemsSource = result;
        }
    }
}
