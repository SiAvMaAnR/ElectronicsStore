using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using ElectronicsShop.Views.Windows;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для ManufacturerPage.xaml
    /// </summary>
    public partial class ManufacturerPage : Page
    {
        public ManufacturerViewModel ManufacturerViewModel = new ManufacturerViewModel();
        public InteractionDataBaseService ManufacturerDataBaseService;
        private readonly string tableName = "[Manufacturer]";

        public ManufacturerPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = ManufacturerViewModel;
            ManufacturerDataBaseService = new ManufacturerDataBaseService(sqlConnection);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManufacturerViewModel.ManufacturerDataTable.Rows.Add();
        }
    }
}
