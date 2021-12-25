using ElectronicsShop.Views.Windows;
using System;
using System.Collections.Generic;
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

        public void UpdateDataTable(DataTable table)
        {
            table.Clear();
            sqlDataAdapter.Fill(table);
        }

        public void CellSelected(DataTable selectionDataTable, string field, object sender)
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

        public async Task<string> GenerateQueryAsync(List<(string,string)> queryValues)
        {
            return await Task.Run(() =>
            {
                try
                {
                    string additionalSqlScript = "";


                    foreach (var item in queryValues)
                    {
                        if (!string.IsNullOrEmpty(item.Item2))
                        {
                            additionalSqlScript += "WHERE ";
                            break;
                        }
                    }

                    for (int i = 0, j = 0; i < queryValues.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(queryValues[i].Item2))
                        {
                            if (j != 0) additionalSqlScript += "AND ";
                            additionalSqlScript += $"{queryValues[i].Item1} LIKE '%{queryValues[i].Item2}%' ";
                            j++;
                        }
                    }
                    return additionalSqlScript;

                }
                catch (Exception ex)
                {
                    return "";
                    throw ex ?? new Exception("Error");
                }
            });
        }
    }
}
