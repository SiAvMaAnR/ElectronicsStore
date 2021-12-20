using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ElectronicsShop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для WaybillPage.xaml
    /// </summary>
    public partial class WaybillPage : Page
    {
        public WaybillViewModel WaybillViewModel = new WaybillViewModel();
        public InteractionDataBaseService WaybillDataBaseService;
        private readonly string tableName = "[Waybill]";

        public WaybillPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = WaybillViewModel;
            WaybillDataBaseService = new WaybillDataBaseService(sqlConnection);
        }

        //Truncate
        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await ((WaybillDataBaseService)WaybillDataBaseService).sqlConnection.OpenAsync();
                await ((WaybillDataBaseService)WaybillDataBaseService).Truncate(tableName);
                ((WaybillDataBaseService)WaybillDataBaseService).UpdateDataTable(WaybillViewModel.WaybillDataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((WaybillDataBaseService)WaybillDataBaseService).sqlConnection.CloseAsync();
            }

        }

        //CreateTable
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((WaybillDataBaseService)WaybillDataBaseService).sqlConnection.OpenAsync();
                await ((WaybillDataBaseService)WaybillDataBaseService).CreateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((WaybillDataBaseService)WaybillDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((WaybillDataBaseService)WaybillDataBaseService).sqlConnection.OpenAsync();
                await ((WaybillDataBaseService)WaybillDataBaseService).DropTable(WaybillDataBaseService.sqlConnection, tableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((WaybillDataBaseService)WaybillDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGridCellTarget = (DataGridCell)sender;
            MessageBox.Show(dataGridCellTarget.Content.ToString());
            MessageBox.Show("Пизда");
            // TODO: Your logic here
        }
    }
}
