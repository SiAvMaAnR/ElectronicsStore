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
    /// <summary>
    /// Логика взаимодействия для SupplierPage.xaml
    /// </summary>
    public partial class SupplierPage : Page
    {
        public SupplierViewModel SupplierViewModel = new SupplierViewModel();
        public InteractionDataBaseService SupplierDataBaseService;
        private readonly string tableName = "[Supplier]";
        public SupplierPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = SupplierViewModel;
            SupplierDataBaseService = new SupplierDataBaseService(sqlConnection);
        }

        //Truncate
        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await ((SupplierDataBaseService)SupplierDataBaseService).sqlConnection.OpenAsync();
                await ((SupplierDataBaseService)SupplierDataBaseService).Truncate(tableName);
                ((SupplierDataBaseService)SupplierDataBaseService).UpdateDataTable(SupplierViewModel.SupplierDataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((SupplierDataBaseService)SupplierDataBaseService).sqlConnection.CloseAsync();
            }

        }

        //CreateTable
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((SupplierDataBaseService)SupplierDataBaseService).sqlConnection.OpenAsync();
                await ((SupplierDataBaseService)SupplierDataBaseService).CreateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((SupplierDataBaseService)SupplierDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((SupplierDataBaseService)SupplierDataBaseService).sqlConnection.OpenAsync();
                await ((SupplierDataBaseService)SupplierDataBaseService).DropTable(SupplierDataBaseService.sqlConnection, tableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((SupplierDataBaseService)SupplierDataBaseService).sqlConnection.CloseAsync();
            }
        }
    }
}
