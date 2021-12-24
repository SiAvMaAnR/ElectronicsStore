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
    public partial class CombinedProductPage : Page
    {
        public CombinedProductViewModel CombinedProductViewModel = new CombinedProductViewModel();

        public Views.Pages.ProductPage productPage;
        public Views.Pages.TypePage typePage;
        public Views.Pages.ManufacturerPage manufacturerPage;

        public CombinedProductPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = CombinedProductViewModel;

            productPage = new Views.Pages.ProductPage(sqlConnection);
            typePage = new Views.Pages.TypePage(sqlConnection);
            manufacturerPage = new Views.Pages.ManufacturerPage(sqlConnection);


            CombinedProductViewModel.ProductPage = productPage;
            CombinedProductViewModel.TypePage = typePage;
            CombinedProductViewModel.ManufacturerPage = manufacturerPage;
        }
    }
}
