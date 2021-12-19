using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectronicsShop.Views.Pages
{
    public partial class CheckPage : Page
    {
        public CheckViewModel CheckViewModel = new CheckViewModel();
        public InteractionDataBaseService CheckDataBaseService;
        private readonly string tableName = "[Check]";

        public CheckPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = CheckViewModel;
            CheckDataBaseService = new CheckDataBaseService(sqlConnection);
        }

        //Truncate
        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.OpenAsync();
                await ((CheckDataBaseService)CheckDataBaseService).Truncate(tableName);
                ((CheckDataBaseService)CheckDataBaseService).UpdateDataTable(CheckViewModel.CheckDataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.CloseAsync();
            }

        }

        //CreateTable
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.OpenAsync();
                await ((CheckDataBaseService)CheckDataBaseService).CreateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.OpenAsync();
                await ((CheckDataBaseService)CheckDataBaseService).DropTable(CheckDataBaseService.sqlConnection, tableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((CheckDataBaseService)CheckDataBaseService).sqlConnection.CloseAsync();
            }
        }
    }
}
