using ElectronicsShop.ViewModels;
using System.Data.SqlClient;
using System.Windows.Controls;

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
