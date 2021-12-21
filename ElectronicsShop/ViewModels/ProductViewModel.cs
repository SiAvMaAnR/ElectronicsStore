using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private DataTable productDataTable = new DataTable();

        public DataTable ProductDataTable
        {
            get { return productDataTable; }
            set
            {
                productDataTable = value;
                OnPropertyChanged(nameof(productDataTable));
            }
        }
    }
}
