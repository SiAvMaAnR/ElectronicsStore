using ElectronicsShop.Models;
using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicsShop.Views.Pages
{
    public partial class TypePage : Page
    {
        public TypeViewModel TypeViewModel = new TypeViewModel();
        public InteractionDataBaseService TypeDataBaseService;
        private SqlConnection sqlConnection;

        public TypePage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = TypeViewModel;
            TypeDataBaseService = new TypeDataBaseService(sqlConnection);
            this.sqlConnection = sqlConnection;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TypeViewModel.TypeDataTable.Rows.Add();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await SearchAsync();
        }


        private async Task SearchAsync()
        {
            try
            {
                string additionalSqlScript = (string.IsNullOrEmpty(TypeViewModel.NameSearch)) ? "" : $"WHERE [Name] LIKE '%{TypeViewModel.NameSearch}%' ";
                TypeViewModel.TypeDataTable = await TypeDataBaseService.GetDataTable(sqlConnection,
                    nameDB: "[dbo].[Type]",
                    additionalSqlScript: additionalSqlScript + "ORDER BY [TypeId] ASC;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
