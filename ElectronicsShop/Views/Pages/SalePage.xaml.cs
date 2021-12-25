﻿using ElectronicsShop.Services;
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
                if (selectedId == 0) throw new Exception("Выберите накладную!");
                DataRowCollection dataRowCollection = SaleViewModel.ProductInCheckDataTable.Rows;
                dataRowCollection.Add();
                dataRowCollection[dataRowCollection.Count-1]["CheckId"] = selectedId;
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
                DataGrid dataGrid = (DataGrid)sender;
                DataRowView dataRowView = (DataRowView)dataGrid.SelectedItem;
                selectedId = (int)dataRowView["CheckId"];

                SaleViewModel.ProductInCheckDataTable = await ProductInCheckDataBaseService.GetDataTable(sqlConnection,
                    nameDB: "[dbo].[ProductInCheck]",
                    additionalSqlScript: $"WHERE [CheckId] = {selectedId};");
            }
            catch
            {

            }
        }
    }
}