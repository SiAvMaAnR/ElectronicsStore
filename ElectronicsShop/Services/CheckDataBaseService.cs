using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class CheckDataBaseService : InteractionDataBaseService
    {
        public CheckDataBaseService(SqlConnection sqlConnection)
            : base(sqlConnection)
        {
        }

        public async Task CreateTable()
        {
            await Task.Run(async () =>
            {
                string sqlScript =
                @$"CREATE TABLE [dbo].[Check](
	            [CheckId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	            [CheckNumber] NVARCHAR(30) NOT NULL UNIQUE NONCLUSTERED,
	            [Date] DATETIME NOT NULL DEFAULT SYSDATETIME(),
	            [ClientId] INT NOT NULL REFERENCES dbo.Client(ClientId) ON DELETE CASCADE);";


                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }
    }
}
