using System;
using System.Data.SqlClient;

namespace TsqltNet
{
    public class SqlTestRunner : ITestRunner
    {
        private readonly ISqlTestExecutor _testExecutor;
        private readonly ITestInstaller _testInstaller;
        private readonly string _connectionString;

        public SqlTestRunner(ISqlTestExecutor testExecutor, ITestInstaller testInstaller, string connectionString)
        {
            if (testExecutor == null) throw new ArgumentNullException(nameof(testExecutor));
            if (testInstaller == null) throw new ArgumentNullException(nameof(testInstaller));
            _testExecutor = testExecutor;
            _testInstaller = testInstaller;
            _connectionString = connectionString;
        }

        public void RunTest(string testClassSchemaName, string testProcedureName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var installedTest = _testInstaller.InstallTest(testClassSchemaName, testProcedureName, connection);
                _testExecutor.RunTest(connection, installedTest);
            }
        }
    }
}