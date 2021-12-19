using ElectronicsShop.Models;
using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicsShop.Views.Pages
{
    public partial class TypePage : Page
    {
        public TypeViewModel TypeViewModel = new TypeViewModel();
        public InteractionDataBaseService TypeDataBaseService;
        private readonly string tableName = "Type";

        public TypePage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = TypeViewModel;
            TypeDataBaseService = new TypeDataBaseService(sqlConnection);
        }

        //Truncate
        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await ((TypeDataBaseService)TypeDataBaseService).sqlConnection.OpenAsync();
                await ((TypeDataBaseService)TypeDataBaseService).Truncate(tableName);
                ((TypeDataBaseService)TypeDataBaseService).UpdateDataTable(TypeViewModel.TypeDataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((TypeDataBaseService)TypeDataBaseService).sqlConnection.CloseAsync();
            }

        }

        //CreateTable
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((TypeDataBaseService)TypeDataBaseService).sqlConnection.OpenAsync();
                await ((TypeDataBaseService)TypeDataBaseService).CreateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((TypeDataBaseService)TypeDataBaseService).sqlConnection.CloseAsync();
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                await ((TypeDataBaseService)TypeDataBaseService).sqlConnection.OpenAsync();
                await ((TypeDataBaseService)TypeDataBaseService).DropTable(TypeDataBaseService.sqlConnection, tableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ((TypeDataBaseService)TypeDataBaseService).sqlConnection.CloseAsync();
            }
        }
    }
}
