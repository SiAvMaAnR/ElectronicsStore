using ElectronicsShop.ViewModels;
using System.Data;
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
    }
}
