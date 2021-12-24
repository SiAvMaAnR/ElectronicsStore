using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class SaleViewModel : BaseViewModel
    {
        private DataTable checkDataTable;

        public DataTable CheckDataTable
        {
            get { return checkDataTable; }
            set
            {
                checkDataTable = value;
                OnPropertyChanged(nameof(CheckDataTable));
            }
        }


        private DataTable productInCheckDataTable;

        public DataTable ProductInCheckDataTable
        {
            get { return productInCheckDataTable; }
            set
            {
                productInCheckDataTable = value;
                OnPropertyChanged(nameof(productInCheckDataTable));
            }
        }
    }
}
