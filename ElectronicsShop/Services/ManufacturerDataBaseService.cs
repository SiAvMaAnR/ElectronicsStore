using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class ManufacturerDataBaseService : InteractionDataBaseService
    {
        public ManufacturerDataBaseService(SqlConnection sqlConnection)
           : base(sqlConnection)
        {
        }

        
    }
}
