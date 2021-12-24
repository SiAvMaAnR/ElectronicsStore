using ElectronicsShop.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ElectronicsShop
{
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;
        private SqlConnection sqlConnection;

        private Views.Pages.ClientPage clientPage;
        
        private Views.Pages.SupplierPage supplierPage;
        private Views.Pages.WaybillPage waybillPage;
        private Views.Pages.ProductInWaybillPage productInWaybillPage;
        private Views.Pages.ProductInStoragePage productInStoragePage;


        private Views.Pages.CombinedProductPage combinedProductPage;
        private Views.Pages.SalePage salePage;


        public MainWindow()
        {
            try
            {
                InitializeComponent();
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDB"].ConnectionString);

                mainViewModel = new MainViewModel();

                clientPage = new Views.Pages.ClientPage(sqlConnection);
                supplierPage = new Views.Pages.SupplierPage(sqlConnection);
                waybillPage = new Views.Pages.WaybillPage(sqlConnection);
                productInWaybillPage = new Views.Pages.ProductInWaybillPage(sqlConnection);
                productInStoragePage = new Views.Pages.ProductInStoragePage(sqlConnection);

                combinedProductPage = new Views.Pages.CombinedProductPage(sqlConnection);
                salePage = new Views.Pages.SalePage(sqlConnection);

                DataContext = mainViewModel;

                mainViewModel.FrameCurrentPage = combinedProductPage;

                FillDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
        }

        private static void Row_Deleted(object sender, DataRowChangeEventArgs e)
        {
            MessageBox.Show("");
        }

        private async void FillDataGrid()
        {
            try
            {
                await sqlConnection.OpenAsync();

                clientPage.ClientViewModel.ClientDataTable = await clientPage.ClientDataBaseService.GetDataTable(sqlConnection, "[dbo].[Client]", "ORDER BY ClientId ASC");
                clientPage.ClientViewModel.ClientDataTable.RowDeleted += Row_Deleted;
                supplierPage.SupplierViewModel.SupplierDataTable = await supplierPage.SupplierDataBaseService.GetDataTable(sqlConnection, "[dbo].[Supplier]", "ORDER BY SupplierId ASC");
                waybillPage.WaybillViewModel.WaybillDataTable = await waybillPage.WaybillDataBaseService.GetDataTable(sqlConnection, "[dbo].[Waybill]", "ORDER BY WaybillId ASC");
                productInWaybillPage.ProductInWaybillViewModel.ProductInWaybillDataTable = await productInWaybillPage.ProductInWaybillDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInWaybill]", "ORDER BY ProductInWaybillId ASC");
                productInStoragePage.ProductInStorageViewModel.ProductInStorageDataTable = await productInStoragePage.ProductInStorageDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInStorage]", "ORDER BY ProductInStorageId ASC");
                
                combinedProductPage.productPage.ProductViewModel.ProductDataTable = await combinedProductPage.productPage.ProductDataBaseService.GetDataTable(sqlConnection, "[dbo].[Product]", "ORDER BY ProductId ASC");
                combinedProductPage.typePage.TypeViewModel.TypeDataTable = await combinedProductPage.typePage.TypeDataBaseService.GetDataTable(sqlConnection, "[dbo].[Type]", "ORDER BY TypeId ASC");
                combinedProductPage.manufacturerPage.ManufacturerViewModel.ManufacturerDataTable = await combinedProductPage.manufacturerPage.ManufacturerDataBaseService.GetDataTable(sqlConnection, "[dbo].[Manufacturer]", "ORDER BY ManufacturerId ASC");

                salePage.SaleViewModel.CheckDataTable = await salePage.CheckDataBaseService.GetDataTable(sqlConnection, "[dbo].[Check]", "ORDER BY CheckId ASC");
                salePage.SaleViewModel.ProductInCheckDataTable = await salePage.ProductInCheckDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInCheck]", "ORDER BY ProductInCheckId ASC");

                waybillPage.SetDataTable(supplierPage.SupplierViewModel.SupplierDataTable);
                productInStoragePage.SetDataTable(combinedProductPage.productPage.ProductViewModel.ProductDataTable);
                combinedProductPage.productPage.SetDataTable(combinedProductPage.typePage.TypeViewModel.TypeDataTable, combinedProductPage.manufacturerPage.ManufacturerViewModel.ManufacturerDataTable);
                productInWaybillPage.SetDataTable(combinedProductPage.productPage.ProductViewModel.ProductDataTable, waybillPage.WaybillViewModel.WaybillDataTable);
                salePage.SetDataTable(clientPage.ClientViewModel.ClientDataTable, productInStoragePage.ProductInStorageViewModel.ProductInStorageDataTable, salePage.SaleViewModel.CheckDataTable);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }


        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await sqlConnection.OpenAsync();

                await combinedProductPage.typePage.TypeDataBaseService.UpdateDataBase(combinedProductPage.typePage.TypeViewModel.TypeDataTable);
                combinedProductPage.typePage.TypeDataBaseService.UpdateDataTable(combinedProductPage.typePage.TypeViewModel.TypeDataTable);

                await clientPage.ClientDataBaseService.UpdateDataBase(clientPage.ClientViewModel.ClientDataTable);
                clientPage.ClientDataBaseService.UpdateDataTable(clientPage.ClientViewModel.ClientDataTable);

                await supplierPage.SupplierDataBaseService.UpdateDataBase(supplierPage.SupplierViewModel.SupplierDataTable);
                supplierPage.SupplierDataBaseService.UpdateDataTable(supplierPage.SupplierViewModel.SupplierDataTable);
                
                await waybillPage.WaybillDataBaseService.UpdateDataBase(waybillPage.WaybillViewModel.WaybillDataTable);
                waybillPage.WaybillDataBaseService.UpdateDataTable(waybillPage.WaybillViewModel.WaybillDataTable);

                await combinedProductPage.manufacturerPage.ManufacturerDataBaseService.UpdateDataBase(combinedProductPage.manufacturerPage.ManufacturerViewModel.ManufacturerDataTable);
                combinedProductPage.manufacturerPage.ManufacturerDataBaseService.UpdateDataTable(combinedProductPage.manufacturerPage.ManufacturerViewModel.ManufacturerDataTable);

                await combinedProductPage.productPage.ProductDataBaseService.UpdateDataBase(combinedProductPage.productPage.ProductViewModel.ProductDataTable);
                combinedProductPage.productPage.ProductDataBaseService.UpdateDataTable(combinedProductPage.productPage.ProductViewModel.ProductDataTable);

                await productInStoragePage.ProductInStorageDataBaseService.UpdateDataBase(productInStoragePage.ProductInStorageViewModel.ProductInStorageDataTable);
                productInStoragePage.ProductInStorageDataBaseService.UpdateDataTable(productInStoragePage.ProductInStorageViewModel.ProductInStorageDataTable);

                await salePage.CheckDataBaseService.UpdateDataBase(salePage.SaleViewModel.CheckDataTable);
                salePage.CheckDataBaseService.UpdateDataTable(salePage.SaleViewModel.CheckDataTable);

                foreach (DataRow row in productInWaybillPage.ProductInWaybillViewModel.ProductInWaybillDataTable.Rows)
                {
                    if (decimal.TryParse(row["Cost"].ToString(), out decimal Cost) && int.TryParse(row["Amount"].ToString(), out int Amount))
                    {
                        row["TotalCost"] = Cost * Amount;
                    }
                }
                await productInWaybillPage.ProductInWaybillDataBaseService.UpdateDataBase(productInWaybillPage.ProductInWaybillViewModel.ProductInWaybillDataTable);
                productInWaybillPage.ProductInWaybillDataBaseService.UpdateDataTable(productInWaybillPage.ProductInWaybillViewModel.ProductInWaybillDataTable);

                foreach (DataRow row in salePage.SaleViewModel.ProductInCheckDataTable.Rows)
                {
                    if (decimal.TryParse(row["Cost"].ToString(), out decimal Cost) && int.TryParse(row["Amount"].ToString(), out int Amount))
                    {
                        row["TotalCost"] = Cost * Amount;
                    }
                }
                await salePage.ProductInCheckDataBaseService.UpdateDataBase(salePage.SaleViewModel.ProductInCheckDataTable);
                salePage.ProductInCheckDataBaseService.UpdateDataTable(salePage.SaleViewModel.ProductInCheckDataTable);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }

        private void ButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = combinedProductPage;
        }

        private void ButtonClient_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = clientPage;
        }

        private void ButtonSupplier_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = supplierPage;
        }

        private void ButtonWaybill_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = waybillPage;
        }

        private void ButtonProductInWaybill_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = productInWaybillPage;
        }

        private void ButtonProductInStorage_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = productInStoragePage;
        }

        private void ButtonSale_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = salePage;
        }
    }
}
