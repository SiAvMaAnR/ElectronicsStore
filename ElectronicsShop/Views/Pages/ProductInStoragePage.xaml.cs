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
    /// <summary>
    /// Логика взаимодействия для ProductInStoragePage.xaml
    /// </summary>
    public partial class ProductInStoragePage : Page
    {
        public ProductInStorageViewModel ProductInStorageViewModel = new ProductInStorageViewModel();
        public InteractionDataBaseService ProductInStorageDataBaseService;
        private SqlConnection sqlConnection;

        public ProductInStoragePage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = ProductInStorageViewModel;
            ProductInStorageDataBaseService = new ProductInStorageDataBaseService(sqlConnection);
            this.sqlConnection = sqlConnection;
        }

        private DataTable selectionDataTable = new DataTable();

        public void SetDataTable(DataTable dataTable)
        {
            selectionDataTable = dataTable;
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string field = "ProductId";

            ProductInStorageDataBaseService.CellSelected(selectionDataTable, field, sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductInStorageViewModel.ProductInStorageDataTable.Rows.Add();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            try
            {
                string additionalSqlScript = await ProductInStorageDataBaseService.GenerateQueryAsync(new List<(string, string)>()
                {
                    ("(SELECT [Name] FROM [dbo].[Type] WHERE [dbo].[Type].[TypeId] = [dbo].[Product].[TypeId]) ", ProductInStorageViewModel.TypeSearch ),
                    ("(SELECT [Name] FROM [dbo].[Manufacturer] WHERE [dbo].[Manufacturer].[ManufacturerId] = [dbo].[Product].[ManufacturerId]) ", ProductInStorageViewModel.ManufacturerSearch ),
                    ("[Model] ", ProductInStorageViewModel.ModelSearch ),
                    ("[Year]", ProductInStorageViewModel.YearSearch ),
                    ("[Color]", ProductInStorageViewModel.ColorSearch ),
                });

                string sqlSqript = $"SELECT * FROM [dbo].[ProductInStorage] WHERE [ProductInStorage].[ProductId] IN (SELECT [ProductId] FROM [dbo].[Product] " + additionalSqlScript + ") " + " ORDER BY [ProductInStorageId] ASC;";


                ProductInStorageViewModel.ProductInStorageDataTable = await ProductInStorageDataBaseService.GetDataTable(sqlConnection, sqlSqript);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
