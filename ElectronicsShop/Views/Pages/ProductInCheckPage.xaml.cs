using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace ElectronicsShop.Views.Pages
{
    public partial class ProductInCheckPage : Page
    {
        public ProductInCheckViewModel ProductInCheckViewModel = new ProductInCheckViewModel();
        public InteractionDataBaseService ProductInCheckDataBaseService;
        private readonly string tableName = "[ProductInCheck]";

        public ProductInCheckPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = ProductInCheckViewModel;
            ProductInCheckDataBaseService = new ProductInCheckDataBaseService(sqlConnection);
        }

        //Truncate
        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await ((ProductInCheckDataBaseService)ProductInCheckDataBaseService).sqlConnection.OpenAsync();
                await ((ProductInCheckDataBaseService)ProductInCheckDataBaseService).Truncate(tableName);
                ((ProductInCheckDataBaseService)ProductInCheckDataBaseService).UpdateDataTable(ProductInCheckViewModel.ProductInCheckDataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((ProductInCheckDataBaseService)ProductInCheckDataBaseService).sqlConnection.CloseAsync();
            }

        }

        //CreateTable
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((ProductInCheckDataBaseService)ProductInCheckDataBaseService).sqlConnection.OpenAsync();
                await ((ProductInCheckDataBaseService)ProductInCheckDataBaseService).CreateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((ProductInCheckDataBaseService)ProductInCheckDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((ProductInCheckDataBaseService)ProductInCheckDataBaseService).sqlConnection.OpenAsync();
                await ((ProductInCheckDataBaseService)ProductInCheckDataBaseService).DropTable(ProductInCheckDataBaseService.sqlConnection, tableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((ProductInCheckDataBaseService)ProductInCheckDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private DataTable selectionDataTable = new DataTable();
        private DataTable selectionDataTable1 = new DataTable();

        public void SetDataTable(DataTable dataTable, DataTable dataTable1)
        {
            selectionDataTable = dataTable;
            selectionDataTable1 = dataTable1;
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string field = "ProductId";

            ProductInCheckDataBaseService.CellSelected(selectionDataTable, field, sender);
        }

        private void DataGridCell_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            string field = "CheckId";

            ProductInCheckDataBaseService.CellSelected(selectionDataTable1, field, sender);
        }
    }
}
