using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElectronicsShop.ViewModels
{
    public class CombinedProductViewModel : BaseViewModel
    {
        private Page productPage;
        public Page ProductPage
        {
            get { return productPage; }
            set
            {
                productPage = value;
                OnPropertyChanged(nameof(productPage));
            }
        }

        private Page typePage;
        public Page TypePage
        {
            get { return typePage; }
            set
            {
                typePage = value;
                OnPropertyChanged(nameof(typePage));
            }
        }

        private Page manufacturerPage;
        public Page ManufacturerPage
        {
            get { return manufacturerPage; }
            set
            {
                manufacturerPage = value;
                OnPropertyChanged(nameof(manufacturerPage));
            }
        }
    }
}
