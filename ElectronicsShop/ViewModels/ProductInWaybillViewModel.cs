using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class ProductInWaybillViewModel:BaseViewModel
    {
        private DataTable productInWaybillDataTable = new DataTable();

        public DataTable ProductInWaybillDataTable
        {
            get { return productInWaybillDataTable; }
            set
            {
                productInWaybillDataTable = value;
                OnPropertyChanged(nameof(productInWaybillDataTable));
            }
        }
    }
}
