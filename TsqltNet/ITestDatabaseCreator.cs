using System.Data.SqlClient;

namespace TsqltNet
{
    public interface ITestDatabaseCreator
    {
        /// <summary>
        /// Creates a database and switches to it
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>The name of the created database</returns>
        string CreateDatabase(SqlConnection connection);
    }
}