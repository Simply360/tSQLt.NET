using System.Data.SqlClient;

namespace TsqltNet
{
    public interface ITestDatabaseInstaller
    {
        /// <summary>
        /// Creates a database and returns a connection string to connect to it.
        /// </summary>
        /// <returns>The name of the created database</returns>
        string InstallTestDatabase(SqlConnection connection);
    }
}