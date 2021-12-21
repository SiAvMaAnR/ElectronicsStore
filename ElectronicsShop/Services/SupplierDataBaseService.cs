using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class SupplierDataBaseService : InteractionDataBaseService
    {
        public SupplierDataBaseService(SqlConnection sqlConnection)
            : base(sqlConnection)
        {
        }

        public async Task CreateTable()
        {
            await Task.Run(async () =>
            {
                string sqlScript =
                @$"CREATE TABLE [dbo].[Supplier] (
	            [SupplierId] INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	            [Name] NVARCHAR(40) NOT NULL UNIQUE NONCLUSTERED,
	            [PhoneNumber] NVARCHAR(25) NULL);";


                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }
    }
}
