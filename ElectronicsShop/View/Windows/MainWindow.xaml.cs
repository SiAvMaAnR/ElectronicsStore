using ElectronicsShop.ViewModels;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace ElectronicsShop
{
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;
        private SqlConnection sqlConnection;
        private View.Pages.TypeTablePage typeTablePage;
        private View.Pages.ClientTablePage clientTablePage;
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDB"].ConnectionString);
                sqlConnection.Open();
                mainViewModel = new MainViewModel();

                typeTablePage = new View.Pages.TypeTablePage();
                clientTablePage = new View.Pages.ClientTablePage();

                DataContext = mainViewModel;

                mainViewModel.FrameCurrentPage = typeTablePage;

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
            typeTablePage.TypeViewModel.TypeDataTable = await typeTablePage.TypeDataBaseService.GetAllDataTable(sqlConnection, "Type");
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
