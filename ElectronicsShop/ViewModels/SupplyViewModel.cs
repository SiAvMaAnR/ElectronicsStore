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
    }
}
