using System;
using System.Data.SqlClient;

namespace TsqltNet
{
    public class TestEnvironmentBootstrapper : ITestEnvironmentBootstrapper
    {
        private readonly ITsqltInstaller _tsqltInstaller;
        private readonly IDbMigrator _dbMigrator;
        private readonly ITestClassDiscoverer _testClassDiscoverer;
        private readonly ITsqltTestClassInstaller _testClassInstaller;

        public TestEnvironmentBootstrapper(ITsqltInstaller tsqltInstaller, IDbMigrator dbMigrator, ITestClassDiscoverer testClassDiscoverer,
            ITsqltTestClassInstaller testClassInstaller)
        {
            if (tsqltInstaller == null) throw new ArgumentNullException(nameof(tsqltInstaller));
            if (dbMigrator == null) throw new ArgumentNullException(nameof(dbMigrator));
            if (testClassDiscoverer == null) throw new ArgumentNullException(nameof(testClassDiscoverer));
            if (testClassInstaller == null) throw new ArgumentNullException(nameof(testClassInstaller));

            _tsqltInstaller = tsqltInstaller;
            _dbMigrator = dbMigrator;
            _testClassDiscoverer = testClassDiscoverer;
            _testClassInstaller = testClassInstaller;
        }

        public void BootstrapEnvironment(string connectionString)
        {
            _tsqltInstaller.Install(connectionString);
            _dbMigrator.Migrate(connectionString);

            var testClasses = _testClassDiscoverer.DiscoverTests();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                foreach (var testClass in testClasses)
                {
                    _testClassInstaller.Install(testClass, sqlConnection);
                }
            }
        }
    }
}
