using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class WaybillDataBaseService : InteractionDataBaseService
    {
        public WaybillDataBaseService(SqlConnection sqlConnection)
            : base(sqlConnection)
        {
        }

        public async Task CreateTable()
        {
            await Task.Run(async () =>
            {
                string sqlScript =
                @$"CREATE TABLE [dbo].[Waybill](
	            [WaybillId]INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
                [WaybillNumber] NVARCHAR(30) NOT NULL UNIQUE NONCLUSTERED,
                [Description] NVARCHAR (200) NULL,
                [Date]  DATETIME DEFAULT SYSDATETIME() NOT NULL,
                [SupplierId] INT NOT NULL REFERENCES [dbo].[Supplier] ([SupplierId]) ON DELETE CASCADE);";


                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }
    }
}
