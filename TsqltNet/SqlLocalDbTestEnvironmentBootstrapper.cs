using System;
using System.Data.SqlClient;
using System.Data.SqlLocalDb;

namespace TsqltNet
{
    public class SqlLocalDbTestEnvironmentBootstrapper : ITestEnvironmentBootstrapper
    {
        private readonly IDbMigrator _dbMigrator;
        private readonly ITestDatabaseInstaller _testDatabaseInstaller;
        private readonly ISqlLocalDbInstance _sqlLocalDbInstance;

        public SqlLocalDbTestEnvironmentBootstrapper(IDbMigrator dbMigrator,
            ITestDatabaseInstaller testDatabaseInstaller, ISqlLocalDbInstance sqlLocalDbInstance)
        {
            if (dbMigrator == null) throw new ArgumentNullException(nameof(dbMigrator));
            if (testDatabaseInstaller == null) throw new ArgumentNullException(nameof(testDatabaseInstaller));
            if (sqlLocalDbInstance == null) throw new ArgumentNullException(nameof(sqlLocalDbInstance));

            _dbMigrator = dbMigrator;
            _testDatabaseInstaller = testDatabaseInstaller;
            _sqlLocalDbInstance = sqlLocalDbInstance;
        }

        public string BootstrapEnvironment()
        {
            var connectionStringBuilder = _sqlLocalDbInstance.CreateConnectionStringBuilder();
            var masterConnectionString = connectionStringBuilder.ToString();
            var databaseName = InstallTestDatabase(masterConnectionString);
            var testDbConnectionString = GetTestDatabaseConnectionString(databaseName);

            _dbMigrator.Migrate(testDbConnectionString);

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

        private string GetTestDatabaseConnectionString(string testDatabaseName)
        {
            var connectionStringBuilder = _sqlLocalDbInstance.CreateConnectionStringBuilder();
            connectionStringBuilder.InitialCatalog = testDatabaseName;
            return connectionStringBuilder.ToString();
        }
    }
}
