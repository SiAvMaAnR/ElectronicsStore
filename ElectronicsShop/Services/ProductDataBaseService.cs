using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class ProductDataBaseService:InteractionDataBaseService
    {
        public ProductDataBaseService(SqlConnection sqlConnection)
           : base(sqlConnection)
        {
        }

       
    }
}
