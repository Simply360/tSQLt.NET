using System.Data.SqlClient;

namespace Tsqlt.Tests
{
    static class TestUtils
    {
        private const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tsqlt.Tests;Integrated Security=True";

        public static SqlConnection GetTestDbConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
