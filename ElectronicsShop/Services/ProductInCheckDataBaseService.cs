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

       
    }
}
