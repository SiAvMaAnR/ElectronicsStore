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

        public TypePage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = TypeViewModel;
            TypeDataBaseService = new TypeDataBaseService(sqlConnection);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TypeViewModel.TypeDataTable.Rows.Add();
        }
    }
}
