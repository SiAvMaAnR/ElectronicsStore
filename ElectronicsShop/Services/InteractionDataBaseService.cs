using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicsShop.Services
{
    public class InteractionDataBaseService
    {
        public SqlDataAdapter sqlDataAdapter;

        public async Task<DataTable> GetDataTable(SqlConnection sqlConnection, string nameDB, string additionalSqlSqcript = "")
        {
            string sqlScript = $"SELECT * FROM {nameDB} {additionalSqlSqcript}";


            DataTable dataTable = new DataTable(nameDB);
            await Task.Run(() =>
            {
                SqlCommand sqlCommand = new SqlCommand(sqlScript, sqlConnection);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
            });

            return dataTable;
        }

        public async Task UpdateDataBase(DataTable table)
        {
            await Task.Run(() =>
            {
                try
                {
                    SqlCommandBuilder commandBuilder1 = new SqlCommandBuilder(sqlDataAdapter);
                    sqlDataAdapter.Update(table);
                }
                catch { throw new Exception("Проверьте корректность введенных данных!"); }
            });
        }

        public async Task UpdateDataTable(DataTable table)
        {
            await Task.Run(() =>
            {
                table.Clear();
                sqlDataAdapter.Fill(table);
            });
        }
    }
}
