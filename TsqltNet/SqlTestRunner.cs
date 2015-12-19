using System;
using System.Data.SqlClient;

namespace TsqltNet
{
    public class SqlTestRunner : ITestRunner
    {
        private readonly ISqlTestExecutor _testExecutor;
        private readonly string _connectionString;

        public SqlTestRunner(ISqlTestExecutor testExecutor, string connectionString)
        {
            if (testExecutor == null) throw new ArgumentNullException(nameof(testExecutor));
            _testExecutor = testExecutor;
            _connectionString = connectionString;
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