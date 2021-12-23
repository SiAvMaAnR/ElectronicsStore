using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class ProductInStorageViewModel: BaseViewModel
    {
        private DataTable productInStorageDataTable = new DataTable();

        public DataTable ProductInStorageDataTable
        {
            get { return productInStorageDataTable; }
            set
            {
                productInStorageDataTable = value;
                OnPropertyChanged(nameof(productInStorageDataTable));
            }
        }
    }
}
