using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class ProductDataBaseService:InteractionDataBaseService
    {
        public ProductDataBaseService(SqlConnection sqlConnection)
           : base(sqlConnection)
        {
        }

        public async Task CreateTable()
        {
            await Task.Run(async () =>
            {
                string sqlScript =
                @$"CREATE TABLE [dbo].[Product](
	            [ProductId] INT IDENTITY(1,1) PRIMARY KEY,
	            [Model] NVARCHAR(60) NOT NULL,
	            [Color] NVARCHAR(20) NOT NULL,
	            [Cost] DECIMAL(10,2) NOT NULL,
	            [Year] INT NOT NULL, 
	            [Weight] FLOAT NOT NULL,
	            [MonthsWarranty] INT NOT NULL,
	            [TypeId] INT NOT NULL REFERENCES [dbo].[Type] ([TypeId]) ON DELETE CASCADE,
	            [ManufacturerId] INT NOT NULL REFERENCES [dbo].[Manufacturer] ([ManufacturerId]) ON DELETE CASCADE);";


                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }
    }
}
