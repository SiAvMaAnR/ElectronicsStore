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
    /// Логика взаимодействия для SalePage.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        public SaleViewModel SaleViewModel = new SaleViewModel();

        public InteractionDataBaseService CheckDataBaseService;
        public InteractionDataBaseService ProductInCheckDataBaseService;
        private SqlConnection sqlConnection;

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
        private DataTable selectionDataTable2 = new DataTable();

        public void SetDataTable(DataTable dataTable, DataTable dataTable1, DataTable dataTable2)
        {
            selectionDataTable = dataTable;
            selectionDataTable1 = dataTable1;
            selectionDataTable2 = dataTable2;
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


        private void DataGridCell_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            string field = "CheckId";

            ProductInCheckDataBaseService.CellSelected(selectionDataTable2, field, sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaleViewModel.CheckDataTable.Rows.Add();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaleViewModel.ProductInCheckDataTable.Rows.Add();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SaleViewModel.ProductInCheckDataTable = await ProductInCheckDataBaseService.GetDataTable(sqlConnection, 
                baseSqlScript: "SELECT ProductInCheckId, Amount FROM", 
                nameDB: "[dbo].[ProductInCheck]", 
                additionalSqlScript: ";");
        }
    }
}
