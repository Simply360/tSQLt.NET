using System;
using System.Data.SqlClient;

namespace TsqltNet
{
    public class TestDatabaseInstaller : ITestDatabaseInstaller
    {
        private readonly ITsqltInstaller _tsqltInstaller;
        private readonly ITestDatabaseCreator _testDatabaseCreator;

        public TestDatabaseInstaller(ITsqltInstaller tsqltInstaller, ITestDatabaseCreator testDatabaseCreator)
        {
            if (tsqltInstaller == null) throw new ArgumentNullException(nameof(tsqltInstaller));
            if (testDatabaseCreator == null) throw new ArgumentNullException(nameof(testDatabaseCreator));
            _tsqltInstaller = tsqltInstaller;
            _testDatabaseCreator = testDatabaseCreator;
        }

        public string InstallTestDatabase(SqlConnection connection)
        {
            var databaseName = _testDatabaseCreator.CreateDatabase(connection);
            _tsqltInstaller.Install(connection);
            return databaseName;
        }
    }
}