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
    }
}
