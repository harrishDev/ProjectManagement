using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.util
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Server=DESKTOP-HNVF699;Database=ProjectManagement;Trusted_Connection=True;TrustServerCertificate=True;";
            return new SqlConnection(connectionString);
        }
    }
}
