using ElectronicsShop.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicsShop.Models
{
    public class TypeDataBaseService : InteractionDataBaseService
    {
        public TypeDataBaseService(SqlConnection sqlConnection)
            :base(sqlConnection)
        {
        }

        public async Task Truncate(string additionalSqlScript = "")
        {
            await Task.Run(async () =>
            {
                string sqlScript = @$"TRUNCATE TABLE TYPE {additionalSqlScript};";
                SqlCommand command = new SqlCommand(sqlScript,sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }

        public async Task CreateTable()
        {
            await Task.Run(async () =>
            {
                string sqlScript =
                @$"CREATE TABLE [dbo].[Type] (
                [TypeId] INT IDENTITY (1, 1) PRIMARY KEY CLUSTERED NOT NULL ,
                [Name] NVARCHAR (40) UNIQUE NONCLUSTERED NOT NULL);";


                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }
    }
}
