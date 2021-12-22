using ElectronicsShop.ViewModels;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicsShop
{
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;
        private SqlConnection sqlConnection;

        private Views.Pages.TypePage typePage;
        private Views.Pages.ClientPage clientPage;
        private Views.Pages.CheckPage checkPage;
        private Views.Pages.SupplierPage supplierPage;
        private Views.Pages.WaybillPage waybillPage;
        private Views.Pages.ManufacturerPage manufacturerPage;
        private Views.Pages.ProductPage productPage;
        private Views.Pages.ProductInCheckPage productInCheck;
        private Views.Pages.ProductInWaybillPage productInWaybill;

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDB"].ConnectionString);

                mainViewModel = new MainViewModel();

                typePage = new Views.Pages.TypePage(sqlConnection);
                clientPage = new Views.Pages.ClientPage(sqlConnection);
                checkPage = new Views.Pages.CheckPage(sqlConnection);
                supplierPage = new Views.Pages.SupplierPage(sqlConnection);
                waybillPage = new Views.Pages.WaybillPage(sqlConnection);
                manufacturerPage = new Views.Pages.ManufacturerPage(sqlConnection);
                productPage = new Views.Pages.ProductPage(sqlConnection);
                productInCheck = new Views.Pages.ProductInCheckPage(sqlConnection);
                productInWaybill = new Views.Pages.ProductInWaybillPage(sqlConnection);

                DataContext = mainViewModel;

                mainViewModel.FrameCurrentPage = typePage;

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
                typePage.TypeViewModel.TypeDataTable = await typePage.TypeDataBaseService.GetDataTable(sqlConnection, "[dbo].[Type]", "ORDER BY TypeId ASC");
                clientPage.ClientViewModel.ClientDataTable = await clientPage.ClientDataBaseService.GetDataTable(sqlConnection, "[dbo].[Client]", "ORDER BY ClientId ASC");
                checkPage.CheckViewModel.CheckDataTable = await checkPage.CheckDataBaseService.GetDataTable(sqlConnection, "[dbo].[Check]", "ORDER BY CheckId ASC");
                supplierPage.SupplierViewModel.SupplierDataTable = await supplierPage.SupplierDataBaseService.GetDataTable(sqlConnection, "[dbo].[Supplier]", "ORDER BY SupplierId ASC");
                waybillPage.WaybillViewModel.WaybillDataTable = await waybillPage.WaybillDataBaseService.GetDataTable(sqlConnection, "[dbo].[Waybill]", "ORDER BY WaybillId ASC");
                manufacturerPage.ManufacturerViewModel.ManufacturerDataTable = await manufacturerPage.ManufacturerDataBaseService.GetDataTable(sqlConnection, "[dbo].[Manufacturer]", "ORDER BY ManufacturerId ASC");
                productPage.ProductViewModel.ProductDataTable = await productPage.ProductDataBaseService.GetDataTable(sqlConnection, "[dbo].[Product]", "ORDER BY ProductId ASC");
                productInCheck.ProductInCheckViewModel.ProductInCheckDataTable = await productInCheck.ProductInCheckDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInCheck]", "ORDER BY ProductInCheckId ASC");
                productInWaybill.ProductInWaybillViewModel.ProductInWaybillDataTable = await productInWaybill.ProductInWaybillDataBaseService.GetDataTable(sqlConnection, "[dbo].[ProductInWaybill]", "ORDER BY ProductInWaybillId ASC");

                waybillPage.SetDataTable(supplierPage.SupplierViewModel.SupplierDataTable);
                checkPage.SetDataTable(clientPage.ClientViewModel.ClientDataTable);
                productPage.SetDataTable(typePage.TypeViewModel.TypeDataTable, manufacturerPage.ManufacturerViewModel.ManufacturerDataTable);
                productInCheck.SetDataTable(productPage.ProductViewModel.ProductDataTable, checkPage.CheckViewModel.CheckDataTable);
                productInWaybill.SetDataTable(productPage.ProductViewModel.ProductDataTable, waybillPage.WaybillViewModel.WaybillDataTable);
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

                await typePage.TypeDataBaseService.UpdateDataBase(typePage.TypeViewModel.TypeDataTable);
                typePage.TypeDataBaseService.UpdateDataTable(typePage.TypeViewModel.TypeDataTable);

                await clientPage.ClientDataBaseService.UpdateDataBase(clientPage.ClientViewModel.ClientDataTable);
                clientPage.ClientDataBaseService.UpdateDataTable(clientPage.ClientViewModel.ClientDataTable);

                await checkPage.CheckDataBaseService.UpdateDataBase(checkPage.CheckViewModel.CheckDataTable);
                checkPage.CheckDataBaseService.UpdateDataTable(checkPage.CheckViewModel.CheckDataTable);

                await supplierPage.SupplierDataBaseService.UpdateDataBase(supplierPage.SupplierViewModel.SupplierDataTable);
                supplierPage.SupplierDataBaseService.UpdateDataTable(supplierPage.SupplierViewModel.SupplierDataTable);

                await waybillPage.WaybillDataBaseService.UpdateDataBase(waybillPage.WaybillViewModel.WaybillDataTable);
                waybillPage.WaybillDataBaseService.UpdateDataTable(waybillPage.WaybillViewModel.WaybillDataTable);

                await manufacturerPage.ManufacturerDataBaseService.UpdateDataBase(manufacturerPage.ManufacturerViewModel.ManufacturerDataTable);
                manufacturerPage.ManufacturerDataBaseService.UpdateDataTable(manufacturerPage.ManufacturerViewModel.ManufacturerDataTable);

                await productPage.ProductDataBaseService.UpdateDataBase(productPage.ProductViewModel.ProductDataTable);
                productPage.ProductDataBaseService.UpdateDataTable(productPage.ProductViewModel.ProductDataTable);

                await productInWaybill.ProductInWaybillDataBaseService.UpdateDataBase(productInWaybill.ProductInWaybillViewModel.ProductInWaybillDataTable);
                productInWaybill.ProductInWaybillDataBaseService.UpdateDataTable(productInWaybill.ProductInWaybillViewModel.ProductInWaybillDataTable);

                await productInCheck.ProductInCheckDataBaseService.UpdateDataBase(productInCheck.ProductInCheckViewModel.ProductInCheckDataTable);
                productInCheck.ProductInCheckDataBaseService.UpdateDataTable(productInCheck.ProductInCheckViewModel.ProductInCheckDataTable);
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



        private void ButtonType_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = typePage;
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

        private void ButtonManufacturer_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = manufacturerPage;
        }

        private void ButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = productPage;
        }

        private void ButtonProductInWaybill_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = productInWaybill;
        }

        private void ButtonProductInCheck_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = productInCheck;
        }

        private void ButtonOther_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
