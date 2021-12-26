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
    public partial class SupplyPage : Page
    {
        public SupplyViewModel SupplyViewModel = new SupplyViewModel();

        public InteractionDataBaseService WaybillDataBaseService;
        public InteractionDataBaseService ProductInWaybillDataBaseService;
        private SqlConnection sqlConnection;
        private int selectedId = 0;

        public SupplyPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            WaybillDataBaseService = new WaybillDataBaseService(sqlConnection);
            ProductInWaybillDataBaseService = new ProductInWaybillDataBaseService(sqlConnection);

            DataContext = SupplyViewModel;
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
            string field = "SupplierId";

            WaybillDataBaseService.CellSelected(selectionDataTable, field, sender);
        }

        private void DataGridCell_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            string field = "ProductId";

            ProductInWaybillDataBaseService.CellSelected(selectionDataTable1, field, sender);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SupplyViewModel.WaybillDataTable.Rows.Add();
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
                if (selectedId == 0) throw new Exception("Выберите накладную!");
                DataRowCollection dataRowCollection = SupplyViewModel.ProductInWaybillDataTable.Rows;
                dataRowCollection.Add();
                dataRowCollection[dataRowCollection.Count - 1]["WaybillId"] = selectedId;
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
                selectedId = (int)dataRowView["WaybillId"];

                SupplyViewModel.ProductInWaybillDataTable = await ProductInWaybillDataBaseService.GetDataTable(sqlConnection,
                    nameDB: "[dbo].[ProductInWaybill]",
                    additionalSqlScript: $"WHERE [WaybillId] = {selectedId};");

                string sqlSqript = $"SELECT SUM(TotalCost) FROM [dbo].[ProductInWaybill] WHERE [WaybillId] = {selectedId};";
                SupplyViewModel.Expenditure = await ProductInWaybillDataBaseService.GetValueFromSql(sqlSqript);
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
                string additionalSqlScript = await WaybillDataBaseService.GenerateQueryAsync(new List<(string, string)>()
                {
                    ("[WaybillNumber]", SupplyViewModel.WaybillSearch ),
                    ("(SELECT [Name] FROM [dbo].[Supplier] WHERE [dbo].[Supplier].[SupplierId] = [dbo].[Waybill].[SupplierId]) ", SupplyViewModel.SupplierSearch ),
                });


                SupplyViewModel.WaybillDataTable = await WaybillDataBaseService.GetDataTable(sqlConnection,
                    nameDB: "[dbo].[Waybill]",
                    additionalSqlScript: additionalSqlScript + "ORDER BY [WaybillId] ASC;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
