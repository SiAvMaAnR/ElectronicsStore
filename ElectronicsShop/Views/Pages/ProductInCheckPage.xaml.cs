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

        

        private DataTable selectionDataTable = new DataTable();
        private DataTable selectionDataTable1 = new DataTable();

        public void SetDataTable(DataTable dataTable, DataTable dataTable1)
        {
            selectionDataTable = dataTable;
            selectionDataTable1 = dataTable1;
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string field = "ProductInStorageId";

            ProductInCheckDataBaseService.CellSelected(selectionDataTable, field, sender);
        }

        private void DataGridCell_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            string field = "CheckId";

            ProductInCheckDataBaseService.CellSelected(selectionDataTable1, field, sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductInCheckViewModel.ProductInCheckDataTable.Rows.Add();
        }
    }
}
