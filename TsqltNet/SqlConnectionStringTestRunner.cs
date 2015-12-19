using System;
using System.Data.SqlClient;

namespace TsqltNet
{
    public class SqlConnectionStringTestRunner : ITestRunner
    {
        private readonly string _connectionString;
        private readonly ISqlTestExecutor _testExecutor;

        public SqlConnectionStringTestRunner(string connectionString, ISqlTestExecutor testExecutor)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            if (testExecutor == null) throw new ArgumentNullException(nameof(testExecutor));
            _connectionString = connectionString;
            _testExecutor = testExecutor;
        }

        public void RunTest(string testClassSchemaName, string testProcedureName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var fullTestName = $"[{testClassSchemaName}].[{testProcedureName}]";
                _testExecutor.RunTest(connection, fullTestName);
            }
        }
    }
}