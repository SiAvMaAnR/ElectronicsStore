using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class ProductInCheckDataBaseService : InteractionDataBaseService
    {
        public ProductInCheckDataBaseService(SqlConnection sqlConnection)
            : base(sqlConnection)
        {
        }

        public async Task CreateTable()
        {
            await Task.Run(async () =>
            {
                string sqlScript =
                @$"CREATE TABLE [dbo].[ProductInCheck](
	            [ProductInCheckId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	            [ProductId] INT NOT NULL REFERENCES [dbo].[Product] ([ProductId]) ON DELETE CASCADE,
	            [CheckId] INT NOT NULL REFERENCES [dbo].[Check] ([CheckId]) ON DELETE CASCADE,
	            [Amount] INT NOT NULL);";


                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }
    }
}
