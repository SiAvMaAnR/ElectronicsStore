using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class ClientViewModel:BaseViewModel
    {
        private DataTable clientDataTable = new DataTable();

        public DataTable ClientDataTable
        {
            get { return clientDataTable; }
            set
            {
                clientDataTable = value;
                OnPropertyChanged(nameof(clientDataTable));
            }
        }
    }
}
