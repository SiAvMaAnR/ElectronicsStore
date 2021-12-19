using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class CheckViewModel : BaseViewModel
    {
        private DataTable checkDataTable = new DataTable();

        public DataTable CheckDataTable
        {
            get { return checkDataTable; }
            set
            {
                checkDataTable = value;
                OnPropertyChanged(nameof(checkDataTable));
            }
        }
    }
}
