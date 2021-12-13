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
        private View.Pages.TypeTablePage typeTablePage;
        private View.Pages.ClientTablePage clientTablePage;
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["shopDB"].ConnectionString);
               
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


        public async Task<MainWindow> Create()
        {
            await sqlConnection.OpenAsync();
            return this;
        }



        private async void FillDataGrid()
        {
            typeTablePage.TypeViewModel.TypeDataTable = await typeTablePage.TypeDataBaseService.GetAllDataTable(sqlConnection, "Type");
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var sqlAdapter = typeTablePage.TypeDataBaseService.sqlDataAdapter;

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Update(typeTablePage.TypeViewModel.TypeDataTable);


        }

    }
}
