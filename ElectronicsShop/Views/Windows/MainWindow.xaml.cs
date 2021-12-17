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


        public async Task<MainWindow> Create()
        {
            await sqlConnection.OpenAsync();
            return this;
        }



        private async void FillDataGrid()
        {
            typePage.TypeViewModel.TypeDataTable = await typePage.TypeDataBaseService.GetAllDataTable(sqlConnection, "Type");
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var sqlAdapter = typePage.TypeDataBaseService.sqlDataAdapter;

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Update(typePage.TypeViewModel.TypeDataTable);


        }

    }
}
