
using System.Data.SqlClient;

namespace ProjectTaskSystem.DAL
{
    public class DBConnection
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(
                @"Data Source=.;Initial Catalog=ProjectTaskDB;Integrated Security=True");
        }
    }
}
