using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectronicsShop.Views.Pages
{
    public partial class ClientPage : Page
    {
        public ClientViewModel ClientViewModel = new ClientViewModel();
        public InteractionDataBaseService ClientDataBaseService;
        private SqlConnection sqlConnection;

        public ClientPage(SqlConnection sqlConnection)
        {
            InitializeComponent();
            DataContext = ClientViewModel;
            ClientDataBaseService = new ClientDataBaseService(sqlConnection);
            this.sqlConnection = sqlConnection; 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientViewModel.ClientDataTable.Rows.Add();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            try
            {
                string additionalSqlScript = await ClientDataBaseService.GenerateQueryAsync(new List<(string, string)>()
                {
                    ("[LastName]", ClientViewModel.LastNameSearch ),
                    ("[FirstName]", ClientViewModel.FirstNameSearch ),
                    ("[MiddleName]", ClientViewModel.MiddleNameSearch ),
                });


                ClientViewModel.ClientDataTable = await ClientDataBaseService.GetDataTable(sqlConnection,
                    nameDB: "[dbo].[Client]",
                    additionalSqlScript: additionalSqlScript + "ORDER BY [ClientId] ASC;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
