using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class SupplierViewModel : BaseViewModel
    {
        private DataTable supplierDataTable = new DataTable();

        public DataTable SupplierDataTable
        {
            get { return supplierDataTable; }
            set
            {
                supplierDataTable = value;
                OnPropertyChanged(nameof(supplierDataTable));
            }
        }
    }
}
