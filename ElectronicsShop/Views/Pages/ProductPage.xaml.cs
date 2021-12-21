using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using ElectronicsShop.Views.Windows;
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
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductViewModel ProductViewModel = new ProductViewModel();
        public InteractionDataBaseService ProductDataBaseService;
        private readonly string tableName = "[Product]";

        public ProductPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = ProductViewModel;
            ProductDataBaseService = new ProductDataBaseService(sqlConnection);
        }

        //Truncate
        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await ((ProductDataBaseService)ProductDataBaseService).sqlConnection.OpenAsync();
                await ((ProductDataBaseService)ProductDataBaseService).Truncate(tableName);
                ((ProductDataBaseService)ProductDataBaseService).UpdateDataTable(ProductViewModel.ProductDataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((ProductDataBaseService)ProductDataBaseService).sqlConnection.CloseAsync();
            }

        }

        //CreateTable
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((ProductDataBaseService)ProductDataBaseService).sqlConnection.OpenAsync();
                await ((ProductDataBaseService)ProductDataBaseService).CreateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((ProductDataBaseService)ProductDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((ProductDataBaseService)ProductDataBaseService).sqlConnection.OpenAsync();
                await ((ProductDataBaseService)ProductDataBaseService).DropTable(ProductDataBaseService.sqlConnection, tableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((ProductDataBaseService)ProductDataBaseService).sqlConnection.CloseAsync();
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
            string field = "TypeId";

            ProductDataBaseService.CellSelected(selectionDataTable, field, sender);
        }

        private void DataGridCell_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            string field = "ManufacturerId";

            ProductDataBaseService.CellSelected(selectionDataTable1,field,sender);
        }
    }
}
