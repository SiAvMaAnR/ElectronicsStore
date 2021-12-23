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
        private readonly string tableName = "[ProductInStorage]";

        public ProductInStoragePage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = ProductInStorageViewModel;
            ProductInStorageDataBaseService = new ProductInStorageDataBaseService(sqlConnection);
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
    }
}
