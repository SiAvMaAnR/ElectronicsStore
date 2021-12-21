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
    }
}
