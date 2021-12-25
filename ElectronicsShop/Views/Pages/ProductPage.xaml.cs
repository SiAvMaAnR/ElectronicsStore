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
    public partial class ProductPage : Page
    {
        public ProductViewModel ProductViewModel = new ProductViewModel();
        public InteractionDataBaseService ProductDataBaseService;
        private SqlConnection sqlConnection;

        public ProductPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = ProductViewModel;
            ProductDataBaseService = new ProductDataBaseService(sqlConnection);
            this.sqlConnection = sqlConnection;
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

            ProductDataBaseService.CellSelected(selectionDataTable1, field, sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductViewModel.ProductDataTable.Rows.Add();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await SearchAsync();
        }


        private async Task SearchAsync()
        {
            try
            {
                string additionalSqlScript = await ProductDataBaseService.GenerateQueryAsync(new List<(string, string)>()
                {
                    
                    ("(SELECT [Name] FROM [dbo].[Type] WHERE [dbo].[Type].TypeId = [dbo].[Product].[TypeId])", ProductViewModel.TypeSearch ),
                    ("(SELECT [Name] FROM [dbo].[Manufacturer] WHERE [dbo].[Manufacturer].ManufacturerId = [dbo].[Product].[ManufacturerId])", ProductViewModel.ManufacturerSearch ),
                    ("[Model]", ProductViewModel.ModelSearch ),
                    ("[Year]", ProductViewModel.YearSearch ),
                    ("[Color]", ProductViewModel.ColorSearch )
                });


                ProductViewModel.ProductDataTable = await ProductDataBaseService.GetDataTable(sqlConnection,
                    nameDB: "[dbo].[Product]",
                    additionalSqlScript: additionalSqlScript + "ORDER BY [ProductId] ASC;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
