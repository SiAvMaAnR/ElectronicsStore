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
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDB"].ConnectionString);

                mainViewModel = new MainViewModel();

                typePage = new Views.Pages.TypePage(sqlConnection);
                clientPage = new Views.Pages.ClientPage(sqlConnection);

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
                typePage.TypeViewModel.TypeDataTable = await typePage.TypeDataBaseService.GetDataTable(sqlConnection, "Type", "ORDER BY TypeId ASC");
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
    }
}
