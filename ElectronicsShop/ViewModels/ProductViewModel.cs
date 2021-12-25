using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private DataTable productDataTable = new DataTable();

        public DataTable ProductDataTable
        {
            get { return productDataTable; }
            set
            {
                productDataTable = value;
                OnPropertyChanged(nameof(productDataTable));
            }
        }


        private string typeSearch = "";

        public string TypeSearch
        {
            get { return typeSearch; }
            set
            {
                typeSearch = value;
                OnPropertyChanged(nameof(typeSearch));
            }
        }

        private string manufacturerSearch = "";

        public string ManufacturerSearch
        {
            get { return manufacturerSearch; }
            set
            {
                manufacturerSearch = value;
                OnPropertyChanged(nameof(manufacturerSearch));
            }
        }

        private string modelSearch = "";

        public string ModelSearch
        {
            get { return modelSearch; }
            set
            {
                modelSearch = value;
                OnPropertyChanged(nameof(modelSearch));
            }
        }

        private string yearSearch = "";

        public string YearSearch
        {
            get { return yearSearch; }
            set
            {
                yearSearch = value;
                OnPropertyChanged(nameof(yearSearch));
            }
        }

        private string colorSearch = "";

        public string ColorSearch
        {
            get { return colorSearch; }
            set
            {
                colorSearch = value;
                OnPropertyChanged(nameof(colorSearch));
            }
        }

    }
}
