using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicsShop.Services
{
    public abstract class InteractionDataBaseService
    {
        public SqlDataAdapter sqlDataAdapter;
        public SqlConnection sqlConnection;

        public InteractionDataBaseService(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public async Task<DataTable> GetDataTable(SqlConnection sqlConnection, string nameDB, string additionalSqlScript = "")
        {
            string sqlScript = $"SELECT * FROM {nameDB} {additionalSqlScript}";


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
                    table.AcceptChanges();

                }
                catch (Exception ex)
                {
                    throw ex ?? new Exception("Неизвестная ошибка!");
                }
            });
        }

        public async Task Truncate(string tableName, string additionalSqlScript = "")
        {
            await Task.Run(async () =>
            {
                string sqlScript = @$"TRUNCATE TABLE {tableName} {additionalSqlScript};";
                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }

        public void UpdateDataTable(DataTable table)
        {
            table.Clear();
            sqlDataAdapter.Fill(table);
        }

        public async Task DropTable(SqlConnection sqlConnection, string tableName)
        {
            await Task.Run(async () =>
            {
                string sqlScript = @$"DROP TABLE {tableName};";

                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }
    }
}
