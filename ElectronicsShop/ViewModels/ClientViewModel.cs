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

        private string firstNameSearch = "";

        public string FirstNameSearch
        {
            get { return firstNameSearch; }
            set
            {
                firstNameSearch = value;
                OnPropertyChanged(nameof(firstNameSearch));
            }
        }


        private string lastNameSearch = "";

        public string LastNameSearch
        {
            get { return lastNameSearch; }
            set
            {
                lastNameSearch = value;
                OnPropertyChanged(nameof(lastNameSearch));
            }
        }


        private string middleNameSearch = "";

        public string MiddleNameSearch
        {
            get { return middleNameSearch; }
            set
            {
                middleNameSearch = value;
                OnPropertyChanged(nameof(middleNameSearch));
            }
        }
    }
}
