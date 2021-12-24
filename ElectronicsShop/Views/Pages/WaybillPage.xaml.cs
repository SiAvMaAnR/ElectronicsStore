using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using ElectronicsShop.Views.Windows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ElectronicsShop.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для WaybillPage.xaml
    /// </summary>
    public partial class WaybillPage : Page
    {
        public WaybillViewModel WaybillViewModel = new WaybillViewModel();
        public InteractionDataBaseService WaybillDataBaseService;
        private readonly string tableName = "[Waybill]";

        public WaybillPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = WaybillViewModel;
            WaybillDataBaseService = new WaybillDataBaseService(sqlConnection);
        }


        private DataTable selectionDataTable = new DataTable();

        public void SetDataTable(DataTable dataTable)
        {
            selectionDataTable = dataTable;
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string field = "SupplierId";

            WaybillDataBaseService.CellSelected(selectionDataTable, field,sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WaybillViewModel.WaybillDataTable.Rows.Add();
        }
    }
}
