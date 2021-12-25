using System.Data;

namespace ElectronicsShop.ViewModels
{
    public class ManufacturerViewModel : BaseViewModel
    {
        private DataTable manufacturerDataTable = new DataTable();

        public DataTable ManufacturerDataTable
        {
            get { return manufacturerDataTable; }
            set
            {
                manufacturerDataTable = value;
                OnPropertyChanged(nameof(manufacturerDataTable));
            }
        }


        private string nameSearch = "";

        public string NameSearch
        {
            get { return nameSearch; }
            set
            {
                nameSearch = value;
                OnPropertyChanged(nameof(nameSearch));
            }
        }


        private string countrySearch = "";

        public string CountrySearch
        {
            get { return countrySearch; }
            set
            {
                countrySearch = value;
                OnPropertyChanged(nameof(nameSearch));
            }
        }


    }
}
