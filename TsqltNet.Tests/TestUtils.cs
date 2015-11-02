using System.Data.SqlClient;

namespace TsqltNet.Tests
{
    static class TestUtils
    {
        private const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TsqltNet.Tests;Integrated Security=True";

        public static SqlConnection GetTestDbConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
