using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
namespace Agent
{
    
    public class Connection
    {
        static string server = ConfigurationManager.ConnectionStrings["server"].ConnectionString.ToString();
        static string db = ConfigurationManager.ConnectionStrings["database"].ConnectionString.ToString();
        static string user = ConfigurationManager.ConnectionStrings["user"].ConnectionString.ToString();
        static string pwd = ConfigurationManager.ConnectionStrings["pwd"].ConnectionString.ToString();
        public static string con = $"server={server}; uid={user}; pwd={pwd}; database={db}";
    }
}
