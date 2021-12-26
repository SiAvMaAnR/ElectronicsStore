using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class SupplyViewModel : BaseViewModel
    {
        private DataTable waybillDataTable;

        public DataTable WaybillDataTable
        {
            get { return waybillDataTable; }
            set
            {
                waybillDataTable = value;
                OnPropertyChanged(nameof(waybillDataTable));
            }
        }


        private DataTable productInWaybillDataTable;

        public DataTable ProductInWaybillDataTable
        {
            get { return productInWaybillDataTable; }
            set
            {
                productInWaybillDataTable = value;
                OnPropertyChanged(nameof(productInWaybillDataTable));
            }
        }
        

        private string waybillSearch = "";

        public string WaybillSearch
        {
            get { return waybillSearch; }
            set
            {
                waybillSearch = value;
                OnPropertyChanged(nameof(waybillSearch));
            }
        }

        private string supplierSearch = "";

        public string SupplierSearch
        {
            get { return supplierSearch; }
            set
            {
                supplierSearch = value;
                OnPropertyChanged(nameof(supplierSearch));
            }
        }


        private decimal expenditure = 0;

        public decimal Expenditure
        {
            get { return expenditure; }
            set
            {
                expenditure = value;
                OnPropertyChanged(nameof(expenditure));
            }
        }
    }
}
