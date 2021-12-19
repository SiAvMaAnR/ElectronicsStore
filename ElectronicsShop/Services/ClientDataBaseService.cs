using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class ClientDataBaseService : InteractionDataBaseService
    {
        public ClientDataBaseService(SqlConnection sqlConnection)
            : base(sqlConnection)
        {
        }

        public async Task CreateTable()
        {
            await Task.Run(async () =>
            {
                string sqlScript =
                @$"CREATE TABLE [dbo].[Client] (
                [ClientId]    INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
                [FirstName]   NVARCHAR(40) NOT NULL,
                [LastName]    NVARCHAR(40) NOT NULL,
                [MiddleName]  NVARCHAR(40) NOT NULL,
                [Age]         INT NOT NULL,
                [PhoneNumber] NVARCHAR(25)  NULL,
                UNIQUE NONCLUSTERED([FirstName] ASC, [LastName] ASC, [MiddleName] ASC));";


                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }
    }
}
