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
                @$"CREATE TABLE [dbo].[Type] (
                [TypeId] INT IDENTITY (1, 1) PRIMARY KEY CLUSTERED NOT NULL ,
                [Name] NVARCHAR (40) UNIQUE NONCLUSTERED NOT NULL);";


                SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
                await command.ExecuteNonQueryAsync();
            });
        }
    }
}
