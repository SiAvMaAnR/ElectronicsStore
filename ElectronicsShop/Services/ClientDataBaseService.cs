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

        public async void Truncate(string additionalSqlScript = "")
        {
            string sqlScript = @$"TRUNCATE TABLE CLIENT {additionalSqlScript};";
            SqlCommand command = new SqlCommand(sqlScript, sqlConnection);
            await command.ExecuteNonQueryAsync();
        }
    }
}
