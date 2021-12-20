using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class WaybillViewModel : BaseViewModel
    {
        private DataTable waybillDataTable = new DataTable();

        public DataTable WaybillDataTable
        {
            get { return waybillDataTable; }
            set
            {
                waybillDataTable = value;
                OnPropertyChanged(nameof(waybillDataTable));
            }
        }
    }
}
