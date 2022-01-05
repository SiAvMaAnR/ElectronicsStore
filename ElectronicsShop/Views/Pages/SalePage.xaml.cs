using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ElectronicsShop.Views.Pages
{
    public partial class SalePage : Page
    {
        public SaleViewModel SaleViewModel = new SaleViewModel();

        public InteractionDataBaseService CheckDataBaseService;
        public InteractionDataBaseService ProductInCheckDataBaseService;
        private SqlConnection sqlConnection;
        private int selectedId = 0;

        public SalePage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            CheckDataBaseService = new CheckDataBaseService(sqlConnection);
            ProductInCheckDataBaseService = new ProductInCheckDataBaseService(sqlConnection);

            DataContext = SaleViewModel;
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
            string field = "ClientId";

            CheckDataBaseService.CellSelected(selectionDataTable, field, sender);
        }

        private void DataGridCell_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            string field = "ProductInStorageId";

            ProductInCheckDataBaseService.CellSelected(selectionDataTable1, field, sender);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaleViewModel.CheckDataTable.Rows.Add();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedId == 0) throw new Exception("Выберите счет!");
                DataRowCollection dataRowCollection = SaleViewModel.ProductInCheckDataTable.Rows;
                dataRowCollection.Add();
                dataRowCollection[dataRowCollection.Count - 1]["CheckId"] = selectedId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                await sqlConnection.OpenAsync();
                DataGrid dataGrid = (DataGrid)sender;
                DataRowView dataRowView = (DataRowView)dataGrid.SelectedItem;

                selectedId = (dataRowView != null) ? (int)dataRowView["CheckId"] : 0;

                SaleViewModel.ProductInCheckDataTable = await ProductInCheckDataBaseService.GetDataTable(sqlConnection,
                    nameDB: "[dbo].[ProductInCheck]",
                    additionalSqlScript: $"WHERE [CheckId] = {selectedId};");

                string sqlSqript = $"SELECT SUM(TotalCost) FROM [dbo].[ProductInCheck] WHERE [CheckId] = {selectedId};";
                SaleViewModel.Income = await ProductInCheckDataBaseService.GetValueFromSql(sqlSqript);
            }
            catch { }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            try
            {
                //Генерируем дополнительную часть запроса, передавая в метод GenerateQueryAsync введенные пользователем данные и запросы, для получения их из бд
                string additionalSqlScript = await CheckDataBaseService.GenerateQueryAsync(new List<(string, string)>()
                {
                    ("[CheckNumber]", SaleViewModel.CheckSearch ),
                    ("(SELECT [LastName] FROM [dbo].[Client] WHERE [dbo].[Client].[ClientId] = [dbo].[Check].[ClientId]) ", SaleViewModel.ClientSearch ),
                });
                //Заполняем таблицу полученными данными из бд
                SaleViewModel.CheckDataTable = await CheckDataBaseService.GetDataTable(sqlConnection,
                    nameDB: "[dbo].[Check]",
                    additionalSqlScript: additionalSqlScript + "ORDER BY [CheckId] ASC;");
            }
            catch { throw; }
        }
    }
}
