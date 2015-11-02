using System;
using System.Data.SqlClient;

namespace Tsqlt
{
    public class BootstrappedTestEnvironment : ITestEnvironment
    {
        private readonly string _connectionString;
        private readonly ISqlTestExecutor _testExecutor;
        private readonly ITestEnvironmentBootstrapper _bootstrapper;
        private readonly object _bootstrapLock;
        private bool IsBootstrapped { get; set; }

        public BootstrappedTestEnvironment(string connectionString, ITestEnvironmentBootstrapper bootstrapper, ISqlTestExecutor testExecutor)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            if (bootstrapper == null) throw new ArgumentNullException(nameof(bootstrapper));
            if (testExecutor == null) throw new ArgumentNullException(nameof(testExecutor));
            _connectionString = connectionString;
            _testExecutor = testExecutor;
            _bootstrapper = bootstrapper;
            _bootstrapLock = new object();
        }

        public void RunTest(string testClassName, string testName)
        {
            lock (_bootstrapLock)
            {
                if (!IsBootstrapped)
                {
                    _bootstrapper.BootstrapEnvironment(_connectionString);
                    IsBootstrapped = true;
                }
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                _testExecutor.RunTest(connection, testName);
            }
        }
    }
}