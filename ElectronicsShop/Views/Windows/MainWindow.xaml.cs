using ElectronicsShop.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicsShop
{
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;
        private SqlConnection sqlConnection;

        private Views.Pages.ClientPage clientPage;

        private Views.Pages.SupplierPage supplierPage;
        private Views.Pages.ProductInStoragePage productInStoragePage;


        private Views.Pages.CombinedProductPage combinedProductPage;
        private Views.Pages.SalePage salePage;
        private Views.Pages.SupplyPage supplyPage;


        public MainWindow()
        {
            try
            {
                InitializeComponent();
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDB"].ConnectionString);

                mainViewModel = new MainViewModel();

                clientPage = new Views.Pages.ClientPage(sqlConnection);
                supplierPage = new Views.Pages.SupplierPage(sqlConnection);
                productInStoragePage = new Views.Pages.ProductInStoragePage(sqlConnection);

                combinedProductPage = new Views.Pages.CombinedProductPage(sqlConnection);
                salePage = new Views.Pages.SalePage(sqlConnection);
                supplyPage = new Views.Pages.SupplyPage(sqlConnection);


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

                clientPage.ClientViewModel.ClientDataTable = await clientPage.ClientDataBaseService.GetDataTable(sqlConnection, "[dbo].[Client]", "ORDER BY ClientId ASC");
                supplierPage.SupplierViewModel.SupplierDataTable = await supplierPage.SupplierDataBaseService.GetDataTable(sqlConnection, "[dbo].[Supplier]", "ORDER BY SupplierId ASC");
                productInStoragePage.ProductInStorageViewModel.ProductInStorageDataTable = await productInStoragePage.ProductInStorageDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInStorage]", "ORDER BY ProductInStorageId ASC");

                combinedProductPage.productPage.ProductViewModel.ProductDataTable = await combinedProductPage.productPage.ProductDataBaseService.GetDataTable(sqlConnection, "[dbo].[Product]", "ORDER BY ProductId ASC");
                combinedProductPage.typePage.TypeViewModel.TypeDataTable = await combinedProductPage.typePage.TypeDataBaseService.GetDataTable(sqlConnection, "[dbo].[Type]", "ORDER BY TypeId ASC");
                combinedProductPage.manufacturerPage.ManufacturerViewModel.ManufacturerDataTable = await combinedProductPage.manufacturerPage.ManufacturerDataBaseService.GetDataTable(sqlConnection, "[dbo].[Manufacturer]", "ORDER BY ManufacturerId ASC");

                salePage.SaleViewModel.CheckDataTable = await salePage.CheckDataBaseService.GetDataTable(sqlConnection, "[dbo].[Check]", "ORDER BY CheckId ASC");
                salePage.SaleViewModel.ProductInCheckDataTable = await salePage.ProductInCheckDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInCheck]", "ORDER BY ProductInCheckId ASC");
                supplyPage.SupplyViewModel.WaybillDataTable = await supplyPage.WaybillDataBaseService.GetDataTable(sqlConnection, "[dbo].[Waybill]", "ORDER BY WaybillId ASC");
                supplyPage.SupplyViewModel.ProductInWaybillDataTable = await supplyPage.ProductInWaybillDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInWaybill]", "ORDER BY ProductInWaybillId ASC");

                productInStoragePage.SetDataTable(combinedProductPage.productPage.ProductViewModel.ProductDataTable);
                combinedProductPage.productPage.SetDataTable(combinedProductPage.typePage.TypeViewModel.TypeDataTable, combinedProductPage.manufacturerPage.ManufacturerViewModel.ManufacturerDataTable);
                salePage.SetDataTable(clientPage.ClientViewModel.ClientDataTable, productInStoragePage.ProductInStorageViewModel.ProductInStorageDataTable);
                supplyPage.SetDataTable(supplierPage.SupplierViewModel.SupplierDataTable, combinedProductPage.productPage.ProductViewModel.ProductDataTable);

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

                await combinedProductPage.manufacturerPage.ManufacturerDataBaseService.UpdateDataBase(combinedProductPage.manufacturerPage.ManufacturerViewModel.ManufacturerDataTable);
                combinedProductPage.manufacturerPage.ManufacturerDataBaseService.UpdateDataTable(combinedProductPage.manufacturerPage.ManufacturerViewModel.ManufacturerDataTable);

                await combinedProductPage.productPage.ProductDataBaseService.UpdateDataBase(combinedProductPage.productPage.ProductViewModel.ProductDataTable);
                combinedProductPage.productPage.ProductDataBaseService.UpdateDataTable(combinedProductPage.productPage.ProductViewModel.ProductDataTable);

                await productInStoragePage.ProductInStorageDataBaseService.UpdateDataBase(productInStoragePage.ProductInStorageViewModel.ProductInStorageDataTable);
                productInStoragePage.ProductInStorageDataBaseService.UpdateDataTable(productInStoragePage.ProductInStorageViewModel.ProductInStorageDataTable);

                await salePage.CheckDataBaseService.UpdateDataBase(salePage.SaleViewModel.CheckDataTable);
                salePage.CheckDataBaseService.UpdateDataTable(salePage.SaleViewModel.CheckDataTable);

                await supplyPage.WaybillDataBaseService.UpdateDataBase(supplyPage.SupplyViewModel.WaybillDataTable);
                supplyPage.WaybillDataBaseService.UpdateDataTable(supplyPage.SupplyViewModel.WaybillDataTable);
                
                await GetTotalCost(salePage.SaleViewModel.ProductInCheckDataTable);
                await salePage.ProductInCheckDataBaseService.UpdateDataBase(salePage.SaleViewModel.ProductInCheckDataTable);
                salePage.ProductInCheckDataBaseService.UpdateDataTable(salePage.SaleViewModel.ProductInCheckDataTable);

                await GetTotalCost(supplyPage.SupplyViewModel.ProductInWaybillDataTable);
                await supplyPage.ProductInWaybillDataBaseService.UpdateDataBase(supplyPage.SupplyViewModel.ProductInWaybillDataTable);
                supplyPage.ProductInWaybillDataBaseService.UpdateDataTable(supplyPage.SupplyViewModel.ProductInWaybillDataTable);
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

        private void ButtonProductInStorage_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = productInStoragePage;
        }

        private void ButtonSale_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = salePage;
        }

        private void ButtonSupply_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = supplyPage;
        }

        public async Task GetTotalCost(DataTable dataTable)
        {
            await Task.Run(() =>
            {
                try
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if ((row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added) && decimal.TryParse(row["Cost"].ToString(), out decimal Cost) && int.TryParse(row["Amount"].ToString(), out int Amount))
                        {
                            row["TotalCost"] = Cost * Amount;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex ?? new Exception("Неизвестная ошибка");
                }
            });
        }
    }
}
