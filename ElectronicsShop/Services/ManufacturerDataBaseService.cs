using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class ManufacturerDataBaseService : InteractionDataBaseService
    {
        public ManufacturerDataBaseService(SqlConnection sqlConnection)
           : base(sqlConnection)
        {
        }

        public async Task CreateTable()
        {
            await Task.Run(async () =>
            {
                string sqlScript =
                @$"CREATE TABLE [dbo].[Manufacturer](
	            [ManufacturerId] INT IDENTITY(1,1) PRIMARY KEY,
	            [Name] NVARCHAR(40) NOT NULL UNIQUE NONCLUSTERED,
	            [Country] NVARCHAR(40) NOT NULL,
	            [PhoneNumber] NVARCHAR(25));";


                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }
    }
}
