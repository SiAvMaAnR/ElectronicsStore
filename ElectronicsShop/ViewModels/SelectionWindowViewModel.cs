using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class SelectionWindowViewModel : BaseViewModel
    {
        private DataTable selectionDataTable = new DataTable();

        public DataTable SelectionDataTable
        {
            get { return selectionDataTable; }
            set
            {
                selectionDataTable = value;
                OnPropertyChanged(nameof(selectionDataTable));
            }
        }
    }
}
