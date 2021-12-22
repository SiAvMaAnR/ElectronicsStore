using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class ProductInCheckViewModel:BaseViewModel
    {
        private DataTable productInCheckDataTable = new DataTable();

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
