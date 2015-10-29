using System;
using System.Data.SqlClient;

namespace Tsqlt
{
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            _connectionString = connectionString;
        }

        public string DatabaseName
        {
            get { return GetLocalTestDatabaseName(); }
        }

        public bool DatabaseExists()
        {

            return DatabaseExists(this.DatabaseName);
        }

        private bool DatabaseExists(string databaseName)
        {
            var result = (int)ExecuteScalar(string.Format("SELECT COUNT(name) FROM master.dbo.sysdatabases WHERE name='{0}'", databaseName));
            return result > 0;
        }

        private object ExecuteScalar(string sql)
        {
            var localMasterConnectionString = GetLocalMasterConnectionString();
            return SqlHelper.ExecuteScalar(sql, localMasterConnectionString);
        }

        public void CreateDatabase()
        {
            ExecuteNonQuery($"CREATE DATABASE {DatabaseName}");
        }

        public void DropDatabase(bool closeConnections = false)
        {
            if (!DatabaseExists(DatabaseName)) return;

            ExecuteNonQuery($"EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'{DatabaseName}'");

            if (closeConnections)
                ExecuteNonQuery($"ALTER DATABASE [{DatabaseName}] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE");

            ExecuteNonQuery($"DROP DATABASE [{DatabaseName}]");
        }

        private object ExecuteNonQuery(string sql)
        {
            var localMasterConnectionString = GetLocalMasterConnectionString();
            return SqlHelper.ExecuteNonQuery(sql, localMasterConnectionString);
        }



        private string GetLocalTestDatabaseName()
        {
            return GetLocalTestDatabaseSqlConnectionStringBuilder().InitialCatalog;
        }

        private string GetLocalMasterConnectionString()
        {
            var configBuilder = GetLocalTestDatabaseSqlConnectionStringBuilder();
            configBuilder.InitialCatalog = "master";
            return configBuilder.ConnectionString;
        }

        private SqlConnectionStringBuilder GetLocalTestDatabaseSqlConnectionStringBuilder()
        {
            return new SqlConnectionStringBuilder(_connectionString);
        }
    }
}
