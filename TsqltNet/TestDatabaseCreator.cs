using System;
using System.Data.SqlClient;
using TsqltNet.SqlUtils;

namespace TsqltNet
{
    public class TestDatabaseCreator : ITestDatabaseCreator
    {
        private readonly string _databaseName;
        private readonly IDatabaseDropper _databaseDropper;
        private readonly ISqlCommandExecutor _sqlCommandExecutor;

        public TestDatabaseCreator(string databaseName, IDatabaseDropper databaseDropper, ISqlCommandExecutor sqlCommandExecutor)
        {
            if (databaseName == null) throw new ArgumentNullException(nameof(databaseName));
            if (databaseDropper == null) throw new ArgumentNullException(nameof(databaseDropper));
            if (sqlCommandExecutor == null) throw new ArgumentNullException(nameof(sqlCommandExecutor));
            _databaseName = databaseName;
            _databaseDropper = databaseDropper;
            _sqlCommandExecutor = sqlCommandExecutor;
        }

        public string CreateDatabase(SqlConnection connection)
        {
            _databaseDropper.DropDatabase(_databaseName, connection, true);
            _sqlCommandExecutor.ExecuteNonQuery(connection, $"CREATE DATABASE [{_databaseName}];");
            _sqlCommandExecutor.ExecuteNonQuery(connection, $"USE [{_databaseName}];");
            _sqlCommandExecutor.ExecuteNonQuery(connection, "ALTER DATABASE CURRENT SET TRUSTWORTHY ON;");
            _sqlCommandExecutor.ExecuteNonQuery(connection, "EXEC sp_configure 'clr enabled', 1; RECONFIGURE;");
            return _databaseName;
        }
    }
}