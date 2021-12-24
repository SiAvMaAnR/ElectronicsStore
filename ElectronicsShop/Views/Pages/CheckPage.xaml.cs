using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using ElectronicsShop.Views.Windows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ElectronicsShop.Views.Pages
{
    public partial class CheckPage : Page
    {
        public CheckViewModel CheckViewModel = new CheckViewModel();
        public InteractionDataBaseService CheckDataBaseService;
        private readonly string tableName = "[Check]";

        public CheckPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = CheckViewModel;
            CheckDataBaseService = new CheckDataBaseService(sqlConnection);
        }

        
        private DataTable selectionDataTable = new DataTable();

        public void SetDataTable(DataTable dataTable)
        {
            selectionDataTable = dataTable;
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string field = "ClientId";

            CheckDataBaseService.CellSelected(selectionDataTable, field,sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckViewModel.CheckDataTable.Rows.Add();
        }
    }
}
