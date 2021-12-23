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

    }
}
