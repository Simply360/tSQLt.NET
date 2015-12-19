using System.Data.SqlClient;

namespace TsqltNet.SqlUtils
{
    public interface IDatabaseDropper
    {
        void DropDatabase(string databaseName, SqlConnection connection, bool closeConnections = false);
    }
}