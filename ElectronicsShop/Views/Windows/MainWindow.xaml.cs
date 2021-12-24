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
        private Views.Pages.CheckPage checkPage;
        private Views.Pages.SupplierPage supplierPage;
        private Views.Pages.WaybillPage waybillPage;
        private Views.Pages.ProductInCheckPage productInCheckPage;
        private Views.Pages.ProductInWaybillPage productInWaybillPage;
        private Views.Pages.ProductInStoragePage productInStoragePage;


        private Views.Pages.CombinedProductPage combinedProductPage;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDB"].ConnectionString);

                mainViewModel = new MainViewModel();

                clientPage = new Views.Pages.ClientPage(sqlConnection);
                checkPage = new Views.Pages.CheckPage(sqlConnection);
                supplierPage = new Views.Pages.SupplierPage(sqlConnection);
                waybillPage = new Views.Pages.WaybillPage(sqlConnection);
                productInCheckPage = new Views.Pages.ProductInCheckPage(sqlConnection);
                productInWaybillPage = new Views.Pages.ProductInWaybillPage(sqlConnection);
                productInStoragePage = new Views.Pages.ProductInStoragePage(sqlConnection);

                combinedProductPage = new Views.Pages.CombinedProductPage(sqlConnection);

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

        private async void FillDataGrid()
        {
            try
            {
                await sqlConnection.OpenAsync();
                combinedProductPage.typePage.TypeViewModel.TypeDataTable = await combinedProductPage.typePage.TypeDataBaseService.GetDataTable(sqlConnection, "[dbo].[Type]", "ORDER BY TypeId ASC");
                clientPage.ClientViewModel.ClientDataTable = await clientPage.ClientDataBaseService.GetDataTable(sqlConnection, "[dbo].[Client]", "ORDER BY ClientId ASC");
                checkPage.CheckViewModel.CheckDataTable = await checkPage.CheckDataBaseService.GetDataTable(sqlConnection, "[dbo].[Check]", "ORDER BY CheckId ASC");
                supplierPage.SupplierViewModel.SupplierDataTable = await supplierPage.SupplierDataBaseService.GetDataTable(sqlConnection, "[dbo].[Supplier]", "ORDER BY SupplierId ASC");
                waybillPage.WaybillViewModel.WaybillDataTable = await waybillPage.WaybillDataBaseService.GetDataTable(sqlConnection, "[dbo].[Waybill]", "ORDER BY WaybillId ASC");
                combinedProductPage.manufacturerPage.ManufacturerViewModel.ManufacturerDataTable = await combinedProductPage.manufacturerPage.ManufacturerDataBaseService.GetDataTable(sqlConnection, "[dbo].[Manufacturer]", "ORDER BY ManufacturerId ASC");
                combinedProductPage.productPage.ProductViewModel.ProductDataTable = await combinedProductPage.productPage.ProductDataBaseService.GetDataTable(sqlConnection, "[dbo].[Product]", "ORDER BY ProductId ASC");
                productInCheckPage.ProductInCheckViewModel.ProductInCheckDataTable = await productInCheckPage.ProductInCheckDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInCheck]", "ORDER BY ProductInCheckId ASC");
                productInWaybillPage.ProductInWaybillViewModel.ProductInWaybillDataTable = await productInWaybillPage.ProductInWaybillDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInWaybill]", "ORDER BY ProductInWaybillId ASC");
                productInStoragePage.ProductInStorageViewModel.ProductInStorageDataTable = await productInStoragePage.ProductInStorageDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInStorage]", "ORDER BY ProductInStorageId ASC");

                waybillPage.SetDataTable(supplierPage.SupplierViewModel.SupplierDataTable);
                productInStoragePage.SetDataTable(combinedProductPage.productPage.ProductViewModel.ProductDataTable);
                checkPage.SetDataTable(clientPage.ClientViewModel.ClientDataTable);
                combinedProductPage.productPage.SetDataTable(combinedProductPage.typePage.TypeViewModel.TypeDataTable, combinedProductPage.manufacturerPage.ManufacturerViewModel.ManufacturerDataTable);
                productInCheckPage.SetDataTable(productInStoragePage.ProductInStorageViewModel.ProductInStorageDataTable, checkPage.CheckViewModel.CheckDataTable);
                productInWaybillPage.SetDataTable(combinedProductPage.productPage.ProductViewModel.ProductDataTable, waybillPage.WaybillViewModel.WaybillDataTable);
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

                await checkPage.CheckDataBaseService.UpdateDataBase(checkPage.CheckViewModel.CheckDataTable);
                checkPage.CheckDataBaseService.UpdateDataTable(checkPage.CheckViewModel.CheckDataTable);

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

                foreach (DataRow row in productInWaybillPage.ProductInWaybillViewModel.ProductInWaybillDataTable.Rows)
                {
                    if (decimal.TryParse(row["Cost"].ToString(), out decimal Cost) && int.TryParse(row["Amount"].ToString(), out int Amount))
                    {
                        row["TotalCost"] = Cost * Amount;
                    }
                }
                await productInWaybillPage.ProductInWaybillDataBaseService.UpdateDataBase(productInWaybillPage.ProductInWaybillViewModel.ProductInWaybillDataTable);
                productInWaybillPage.ProductInWaybillDataBaseService.UpdateDataTable(productInWaybillPage.ProductInWaybillViewModel.ProductInWaybillDataTable);


                foreach (DataRow row in productInCheckPage.ProductInCheckViewModel.ProductInCheckDataTable.Rows)
                {
                    if (decimal.TryParse(row["Cost"].ToString(), out decimal Cost) && int.TryParse(row["Amount"].ToString(), out int Amount))
                    {
                        row["TotalCost"] = Cost * Amount;
                    }
                }
                await productInCheckPage.ProductInCheckDataBaseService.UpdateDataBase(productInCheckPage.ProductInCheckViewModel.ProductInCheckDataTable);
                productInCheckPage.ProductInCheckDataBaseService.UpdateDataTable(productInCheckPage.ProductInCheckViewModel.ProductInCheckDataTable);
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

        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = checkPage;
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

        private void ButtonProductInCheck_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = productInCheckPage;
        }

        private void ButtonProductInStorage_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = productInStoragePage;
        }
    }
}
