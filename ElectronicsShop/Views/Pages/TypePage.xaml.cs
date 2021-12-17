using ElectronicsShop.Models;
using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System.Windows.Controls;

namespace ElectronicsShop.Views.Pages
{
    public partial class TypePage : Page
    {
        public TypeViewModel TypeViewModel = new TypeViewModel();
        public InteractionDataBaseService TypeDataBaseService = new TypeDataBaseService();

        public TypePage()
        {
            InitializeComponent();
            DataContext = TypeViewModel;
        }
    }
}
