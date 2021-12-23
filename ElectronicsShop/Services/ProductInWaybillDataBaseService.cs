using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class ProductInWaybillDataBaseService : InteractionDataBaseService
    {
        public ProductInWaybillDataBaseService(SqlConnection sqlConnection)
            : base(sqlConnection)
        {
        }

    }
}
