using System;
using System.Data.SqlClient;

namespace TsqltNet.SqlUtils
{
    public class DatabaseDropper : IDatabaseDropper
    {
        private readonly ISqlCommandExecutor _executor;

        public DatabaseDropper(ISqlCommandExecutor executor)
        {
            if (executor == null) throw new ArgumentNullException(nameof(executor));
            _executor = executor;
        }

        public void DropDatabase(string databaseName, SqlConnection connection, bool closeConnections = false)
        {
            if (!DatabaseExists(databaseName, connection)) return;

            _executor.ExecuteNonQuery(connection, $"EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'{databaseName}'");

            if (closeConnections)
                _executor.ExecuteNonQuery(connection, $"ALTER DATABASE [{databaseName}] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE");

            _executor.ExecuteNonQuery(connection, $"DROP DATABASE [{databaseName}]");
        }

        private bool DatabaseExists(string databaseName, SqlConnection connection)
        {
            var numDatabases =
                (int) _executor.ExecuteScalar(connection, $"SELECT COUNT(name) FROM master.dbo.sysdatabases WHERE name='{databaseName}'");
            return numDatabases > 0;
        }
    }
}