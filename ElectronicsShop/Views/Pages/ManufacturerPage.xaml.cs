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
    public partial class ManufacturerPage : Page
    {
        public ManufacturerViewModel ManufacturerViewModel = new ManufacturerViewModel();
        public InteractionDataBaseService ManufacturerDataBaseService;
        private SqlConnection sqlConnection;


        public ManufacturerPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = ManufacturerViewModel;
            ManufacturerDataBaseService = new ManufacturerDataBaseService(sqlConnection);
            this.sqlConnection = sqlConnection;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManufacturerViewModel.ManufacturerDataTable.Rows.Add();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            try
            {
                string additionalSqlScript = await ManufacturerDataBaseService.GenerateQueryAsync(new List<(string, string)>()
                {
                    ("[Name]", ManufacturerViewModel.NameSearch ),
                    ("[Country]", ManufacturerViewModel.CountrySearch ),
                });


                ManufacturerViewModel.ManufacturerDataTable = await ManufacturerDataBaseService.GetDataTable(sqlConnection,
                    nameDB: "[dbo].[Manufacturer]",
                    additionalSqlScript: additionalSqlScript + "ORDER BY [ManufacturerId] ASC;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
