using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using ElectronicsShop.Views.Windows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ElectronicsShop.Views.Pages
{
    public partial class CheckPage : Page
    {
        public CheckViewModel CheckViewModel = new CheckViewModel();
        public InteractionDataBaseService CheckDataBaseService;
        private readonly string tableName = "[Check]";

        public CheckPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = CheckViewModel;
            CheckDataBaseService = new CheckDataBaseService(sqlConnection);
        }

        //Truncate
        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.OpenAsync();
                await ((CheckDataBaseService)CheckDataBaseService).Truncate(tableName);
                ((CheckDataBaseService)CheckDataBaseService).UpdateDataTable(CheckViewModel.CheckDataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.CloseAsync();
            }

        }

        //CreateTable
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.OpenAsync();
                await ((CheckDataBaseService)CheckDataBaseService).CreateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.OpenAsync();
                await ((CheckDataBaseService)CheckDataBaseService).DropTable(CheckDataBaseService.sqlConnection, tableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private DataTable selectionDataTable = new DataTable();

        public void SetDataTable(DataTable dataTable)
        {
            selectionDataTable = dataTable;
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string field = "ClientId";

            CheckDataBaseService.CellSelected(selectionDataTable, field,sender);
        }
    }
}
