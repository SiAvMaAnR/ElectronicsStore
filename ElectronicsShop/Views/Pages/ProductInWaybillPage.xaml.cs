﻿using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ElectronicsShop.Views.Pages
{
    public partial class ProductInWaybillPage : Page
    {
        public ProductInWaybillViewModel ProductInWaybillViewModel = new ProductInWaybillViewModel();
        public InteractionDataBaseService ProductInWaybillDataBaseService;
        private readonly string tableName = "[ProductInWaybill]";

        public ProductInWaybillPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = ProductInWaybillViewModel;
            ProductInWaybillDataBaseService = new ProductInWaybillDataBaseService(sqlConnection);
        }

        //Truncate
        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await ((ProductInWaybillDataBaseService)ProductInWaybillDataBaseService).sqlConnection.OpenAsync();
                await ((ProductInWaybillDataBaseService)ProductInWaybillDataBaseService).Truncate(tableName);
                ((ProductInWaybillDataBaseService)ProductInWaybillDataBaseService).UpdateDataTable(ProductInWaybillViewModel.ProductInWaybillDataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((ProductInWaybillDataBaseService)ProductInWaybillDataBaseService).sqlConnection.CloseAsync();
            }

        }

        //CreateTable
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((ProductInWaybillDataBaseService)ProductInWaybillDataBaseService).sqlConnection.OpenAsync();
                await ((ProductInWaybillDataBaseService)ProductInWaybillDataBaseService).CreateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((ProductInWaybillDataBaseService)ProductInWaybillDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((ProductInWaybillDataBaseService)ProductInWaybillDataBaseService).sqlConnection.OpenAsync();
                await ((ProductInWaybillDataBaseService)ProductInWaybillDataBaseService).DropTable(ProductInWaybillDataBaseService.sqlConnection, tableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((ProductInWaybillDataBaseService)ProductInWaybillDataBaseService).sqlConnection.CloseAsync();
            }
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
            string field = "ProductId";

            ProductInWaybillDataBaseService.CellSelected(selectionDataTable, field, sender);
        }

        private void DataGridCell_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            string field = "WaybillId";

            ProductInWaybillDataBaseService.CellSelected(selectionDataTable1, field, sender);
        }
    }
}
