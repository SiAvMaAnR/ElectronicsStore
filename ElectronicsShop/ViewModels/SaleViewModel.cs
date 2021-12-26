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
                OnPropertyChanged(nameof(checkDataTable));
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


        private string checkSearch = "";

        public string CheckSearch
        {
            get { return checkSearch; }
            set
            {
                checkSearch = value;
                OnPropertyChanged(nameof(checkSearch));
            }
        }

        private string clientSearch = "";

        public string ClientSearch
        {
            get { return clientSearch; }
            set
            {
                clientSearch = value;
                OnPropertyChanged(nameof(clientSearch));
            }
        }

        private decimal income = 0;

        public decimal Income
        {
            get { return income; }
            set
            {
                income = value;
                OnPropertyChanged(nameof(income));
            }
        }
    }
}
