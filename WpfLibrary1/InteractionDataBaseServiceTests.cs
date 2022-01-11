using ElectronicsShop.Models;
using ElectronicsShop.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Xunit;

namespace ElectronicsShop.Tests
{
    public class InteractionDataBaseServiceTests
    {

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-FV1UJ51\MSSQLSERVERN1;Initial Catalog=ElectronicsStore;Integrated Security=True");

        [Fact]
        public async void GetDataTableTest()
        {
            sqlConnection.Open();
            InteractionDataBaseService interactionDataBaseService = new TypeDataBaseService(sqlConnection);

            DataTable dataTable = await interactionDataBaseService.GetDataTable(sqlConnection, "SELECT * FROM [dbo].[Type];");
            Assert.NotNull(dataTable);
            Assert.NotEmpty(dataTable.AsEnumerable());
        }
        
    }
}
