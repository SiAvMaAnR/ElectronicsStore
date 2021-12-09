using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicsShop.Services
{
    public class InteractionDataBaseService
    {
        public async Task<DataTable> GetAllDataTable(SqlConnection sqlConnection, string nameDB)
        {
            string sqlScript = $"SELECT * FROM {nameDB}";

            DataTable dataTable = new DataTable("Type");
            await Task.Run(() =>
            {
                SqlCommand sqlCommand = new SqlCommand(sqlScript, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
            });


            string sum = "";
            foreach (DataRow item in dataTable.Rows)
            {
                var cells = item.ItemArray;
                foreach (var cell in cells)
                {
                    sum += cell?.ToString() + " ";
                }
                sum += "\n";
            }

            MessageBox.Show(sum);
            return dataTable;
        }



    }
}
