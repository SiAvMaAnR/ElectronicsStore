using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicsShop.Views.Pages
{
    public partial class SupplierPage : Page
    {
        public SupplierViewModel SupplierViewModel = new SupplierViewModel();
        public InteractionDataBaseService SupplierDataBaseService;
        private SqlConnection sqlConnection;
        public SupplierPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = SupplierViewModel;
            SupplierDataBaseService = new SupplierDataBaseService(sqlConnection);
            this.sqlConnection = sqlConnection;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SupplierViewModel.SupplierDataTable.Rows.Add();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            try
            {
                string additionalSqlScript = await SupplierDataBaseService.GenerateQueryAsync(new List<(string, string)>()
                {
                    ("[Name]", SupplierViewModel.NameSearch ),
                    ("[City]", SupplierViewModel.CitySearch ),
                });


                SupplierViewModel.SupplierDataTable = await SupplierDataBaseService.GetDataTable(sqlConnection,
                    nameDB: "[dbo].[Supplier]",
                    additionalSqlScript: additionalSqlScript + "ORDER BY [SupplierId] ASC;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
