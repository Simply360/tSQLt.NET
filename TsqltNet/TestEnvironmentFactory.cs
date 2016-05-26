using System;
using System.Data.SqlLocalDb;
using System.Diagnostics;
using System.Reflection;
using TsqltNet.SqlUtils;
using TsqltNet.Testing;

namespace TsqltNet
{
    public static class TestEnvironmentFactory
    {
        public static ITestEnvironment CreateWithSqlLocalDb(Assembly testAssembly, string testResourcePath, IDbMigrator dbMigrator = null, string sqlLocalDbInstanceName = null)
        {
            var sqlLocalDbInstance = GetSqlLocalDbInstance(sqlLocalDbInstanceName ?? "TsqltNet");
            
            sqlLocalDbInstance.Start();
            Debug.WriteLine("Started local DB instance.");
            Func<string, ITestRunner> testRunnerFactory = connectionString =>
            {
                var sqlTestExecutor = GetSqlTestExecutor();
                var testInstaller = GetTestInstaller(testAssembly, testResourcePath);
                return new SqlTestRunner(sqlTestExecutor, testInstaller, connectionString);
            };
            var bootstrapper = GetBootstrapper(dbMigrator, sqlLocalDbInstance);
            return new BootstrappedTestEnvironment(bootstrapper, testRunnerFactory);
        }

        private static ISqlLocalDbInstance GetSqlLocalDbInstance(string instanceName)
        {
            var sqlLocalDbProvider = new SqlLocalDbProvider();
            return sqlLocalDbProvider.GetOrCreateInstance(instanceName);
        }

        private const string DatabaseName = "TsqltTestDb";

        private static ITestEnvironmentBootstrapper GetBootstrapper(IDbMigrator dbMigrator, ISqlLocalDbInstance sqlLocalDbInstance)
        {
            // Poor-man's Dependency Injection
            var embeddedTextResourceReader = new EmbeddedTextResourceReader();
            var sqlBatchExtractor = new SqlBatchExtractor();
            var tsqltInstaller = new EmbeddedResourceTsqltInstaller(embeddedTextResourceReader, sqlBatchExtractor);
            var sqlCommandExecutor = new SqlCommandExecutor();
            var databaseDropper = new DatabaseDropper(sqlCommandExecutor);
            var testDatabaseCreator = new TestDatabaseCreator(DatabaseName, databaseDropper, sqlCommandExecutor);
            var testDatabaseInstaller = new TestDatabaseInstaller(tsqltInstaller, testDatabaseCreator);
            return new SqlLocalDbTestEnvironmentBootstrapper(dbMigrator ?? new DefaultDbMigrator(),
                testDatabaseInstaller, sqlLocalDbInstance);
        }

        private static ISqlTestExecutor GetSqlTestExecutor()
        {
            var outputMessageWriter = new DebugTestOutputMessageWriter();
            return new MessageWritingSqlTestExecutor(outputMessageWriter, new SqlTestExecutor());
        }

        private static ITestInstaller GetTestInstaller(Assembly testAssembly, string testResourcePath)
        {
            var embeddedTextResourceReader = new EmbeddedTextResourceReader();
            var testClassDiscoverer = new AssemblyResourceTestClassDiscoverer(testAssembly, testResourcePath, embeddedTextResourceReader);
            var testClassInstaller = new TsqltTestClassInstaller(embeddedTextResourceReader);
            return new TestInstaller(testClassDiscoverer, testClassInstaller);
        }
    }
}