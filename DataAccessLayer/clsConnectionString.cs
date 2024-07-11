using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsConnectionString
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    }
}
