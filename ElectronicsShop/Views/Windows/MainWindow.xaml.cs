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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FrameCurrentPage = supplierPage;
        }
    }
}
