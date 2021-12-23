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

    }
}
