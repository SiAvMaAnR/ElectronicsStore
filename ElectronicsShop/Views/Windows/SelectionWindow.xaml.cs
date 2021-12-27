using ElectronicsShop.ViewModels;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicsShop.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для SelectionWindow.xaml
    /// </summary>
    public partial class SelectionWindow : Window
    {
        public SelectionWindowViewModel SelectionWindowViewModel;
        private string field = "";
        private DataRowView dataRowView;

        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public int selectedIndex;

        public SelectionWindow(DataTable dataTable, string field, DataRowView dataRowView)
        {
            InitializeComponent();
            SelectionWindowViewModel = new SelectionWindowViewModel();
            DataContext = SelectionWindowViewModel;


            this.field = field;
            this.dataRowView = dataRowView;

            SelectionWindowViewModel.SelectionDataTable = dataTable.Copy();

            
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DataGridRow dataGridRow = (DataGridRow)sender;
                DataRowView dataRowView = (DataRowView)dataGridRow.DataContext;
                this.dataRowView[field] = int.Parse(dataRowView[field]?.ToString() ?? "");
            }
            finally
            {
                this.DialogResult = true;
            }
        }

        private void SelectionDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            int Id = int.Parse(dataRowView[field]?.ToString() ?? "");
            
            selectedIndex = new DataView(SelectionWindowViewModel.SelectionDataTable)
            .ToTable(false, new[] { field })
            .AsEnumerable()
            .Select(row => row.Field<int>(field))
            .ToList()
            .FindIndex(col => col == Id);

            SelectionDataGrid.SelectedIndex = selectedIndex;
            SelectionDataGrid.Focus();
        }
    }
}
