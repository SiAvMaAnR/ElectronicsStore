using ElectronicsShop.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicsShop.Models
{
    public class TypeDataBaseService : InteractionDataBaseService
    {
        public TypeDataBaseService(SqlConnection sqlConnection)
            :base(sqlConnection)
        {
        }

    }
}
