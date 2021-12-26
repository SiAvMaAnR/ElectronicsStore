using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

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
