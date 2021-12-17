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

                typePage = new Views.Pages.TypePage();
                clientPage = new Views.Pages.ClientPage();

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




                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await sqlConnection.OpenAsync();

                await typePage.TypeDataBaseService.UpdateDataBase(typePage.TypeViewModel.TypeDataTable);
                await typePage.TypeDataBaseService.UpdateDataTable(typePage.TypeViewModel.TypeDataTable);

                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
