using ElectronicsShop.Models;
using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System.Windows.Controls;

namespace ElectronicsShop.View.Pages
{
    public partial class TypeTablePage : Page
    {
        public TypeViewModel TypeViewModel = new TypeViewModel();
        public TypeDataBaseService TypeDataBaseService = new TypeDataBaseService();

        public TypeTablePage()
        {
            InitializeComponent();
            DataContext = TypeViewModel;
        }
    }
}
