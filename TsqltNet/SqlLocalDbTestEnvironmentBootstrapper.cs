using System;
using System.Data.SqlClient;
using System.Data.SqlLocalDb;
using TsqltNet.Testing;

namespace TsqltNet
{
    public class SqlLocalDbTestEnvironmentBootstrapper : ITestEnvironmentBootstrapper
    {
        private readonly IDbMigrator _dbMigrator;
        private readonly ITestClassDiscoverer _testClassDiscoverer;
        private readonly ITsqltTestClassInstaller _testClassInstaller;
        private readonly ITestDatabaseInstaller _testDatabaseInstaller;
        private readonly ISqlLocalDbInstance _sqlLocalDbInstance;

        public SqlLocalDbTestEnvironmentBootstrapper(IDbMigrator dbMigrator, ITestClassDiscoverer testClassDiscoverer,
            ITsqltTestClassInstaller testClassInstaller, ITestDatabaseInstaller testDatabaseInstaller, ISqlLocalDbInstance sqlLocalDbInstance)
        {
            if (dbMigrator == null) throw new ArgumentNullException(nameof(dbMigrator));
            if (testClassDiscoverer == null) throw new ArgumentNullException(nameof(testClassDiscoverer));
            if (testClassInstaller == null) throw new ArgumentNullException(nameof(testClassInstaller));
            if (testDatabaseInstaller == null) throw new ArgumentNullException(nameof(testDatabaseInstaller));
            if (sqlLocalDbInstance == null) throw new ArgumentNullException(nameof(sqlLocalDbInstance));

            _dbMigrator = dbMigrator;
            _testClassDiscoverer = testClassDiscoverer;
            _testClassInstaller = testClassInstaller;
            _testDatabaseInstaller = testDatabaseInstaller;
            _sqlLocalDbInstance = sqlLocalDbInstance;
        }

        public string BootstrapEnvironment()
        {
            var connectionStringBuilder = _sqlLocalDbInstance.CreateConnectionStringBuilder();
            var masterConnectionString = connectionStringBuilder.ToString();
            var databaseName = InstallTestDatabase(masterConnectionString);
            var testDbConnectionString = GetTestDatabaaseConnectionString(databaseName);

            _dbMigrator.Migrate(testDbConnectionString);

            var testClasses = _testClassDiscoverer.DiscoverTests();

            using (var connection = new SqlConnection(testDbConnectionString))
            {
                connection.Open();
                foreach (var testClass in testClasses)
                {
                    _testClassInstaller.Install(testClass, connection);
                }
            }

            return testDbConnectionString;
        }

        private string InstallTestDatabase(string masterConnectionString)
        {
            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                return _testDatabaseInstaller.InstallTestDatabase(connection);
            }
        }

        private string GetTestDatabaaseConnectionString(string testDatabaseName)
        {
            var connectionStringBuilder = _sqlLocalDbInstance.CreateConnectionStringBuilder();
            connectionStringBuilder.InitialCatalog = testDatabaseName;
            return connectionStringBuilder.ToString();
        }
    }
}
