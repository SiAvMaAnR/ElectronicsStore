using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElectronicsShop.ViewModels
{
    public class TypeViewModel : BaseViewModel
    {
        private DataTable typeDataTable = new DataTable();

        public DataTable TypeDataTable
        {
            get { return typeDataTable; }
            set
            {
                typeDataTable = value;
                OnPropertyChanged(nameof(typeDataTable));
            }
        }
    }
}
