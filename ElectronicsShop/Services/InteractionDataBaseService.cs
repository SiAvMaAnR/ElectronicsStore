using ElectronicsShop.Views.Windows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        public async Task<DataTable> GetDataTable(SqlConnection sqlConnection, string nameDB, string additionalSqlScript = "", string baseSqlScript = "SELECT * FROM")
        {
            string sqlScript = $"{baseSqlScript} {nameDB} {additionalSqlScript}";


            DataTable dataTable = new DataTable(nameDB);
            await Task.Run(() =>
            {
                dataTable.AcceptChanges();
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


        public void CellSelected(DataTable selectionDataTable,string field, object sender)
        {
            DataGridCell dataGridCell = (DataGridCell)sender;
            DataRowView dataRowView = null;
            try
            {
                dataRowView = (DataRowView)dataGridCell.DataContext;
                SelectionWindow selectionWindow = new SelectionWindow(selectionDataTable, field, dataRowView);
                selectionWindow.ShowDialog();
            }
            catch { }
        }
    }
}
