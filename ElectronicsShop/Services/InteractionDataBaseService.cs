using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicsShop.Services
{
    public class InteractionDataBaseService
    {
        public SqlDataAdapter sqlDataAdapter;

        public async Task<DataTable> GetAllDataTable(SqlConnection sqlConnection, string nameDB)
        {
            string sqlScript = $"SELECT * FROM {nameDB};";

            DataTable dataTable = new DataTable(nameDB);
            await Task.Run(() =>
            {
                SqlCommand sqlCommand = new SqlCommand(sqlScript, sqlConnection);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
            });

            return dataTable;
        }
    }
}
