using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tsqlt
{
    public class BootstrapForTests
    {
        private static bool _alreadyBootstrapped;
        private static readonly object Lock = new object();
        private static readonly ResourcesHelper ResourcesHelper = new ResourcesHelper();

        public static string ConnectionString { get; set; }
        public static string EmbeddedResourcePrefix { get; set; }
        public static Assembly TestAssembly { get; set; }
        public static IDbMigrator Migrator { get; set; }

        public static void BootstrapTestsDatabase()
        {
            lock (Lock)
            {
                if (_alreadyBootstrapped) return;
                SetupTestDatabase();
                PrepareForTsqlt();
                InstallTsqlt();
                ApplyPatches();
                ApplyExistingTests(TestAssembly);
                ApplyWorkInProgress(TestAssembly);
                _alreadyBootstrapped = true;
            }
        }

        public static void ApplyWorkInProgress(Assembly assembly)
        {
            var resourceScripts = GetResourceScriptsForWorkInProgress(assembly);
            TsqltTestUtils.ApplyEmbeddedResourceScripts(resourceScripts, GetLocalTestDatabaseConnectionString());
        }

        private static IEnumerable<string> GetResourceScriptsForWorkInProgress(Assembly assembly)
        {
            var embeddedResources = assembly.GetManifestResourceNames();
            var workInProgressResourceNames = embeddedResources.Where(s => s.StartsWith("tSQLt.WorkInProgress")).OrderBy(s => s).ToList();
            var resourceScripts = ResourcesHelper.GetEmbeddedResourceScriptsFrom(assembly, workInProgressResourceNames);
            return resourceScripts;
        }

        private static void ApplyExistingTests(Assembly assembly)
        {
            var resourceScripts = GetResourceScriptsForTests(assembly);
            TsqltTestUtils.ApplyEmbeddedResourceScripts(resourceScripts, GetLocalTestDatabaseConnectionString());
        }

        private static IEnumerable<string> GetResourceScriptsForTests(Assembly assembly)
        {
            var embeddedResources = assembly.GetManifestResourceNames();
            var testResourceNames = embeddedResources.Where(s => s.StartsWith(EmbeddedResourcePrefix));
            var resourceScripts = ResourcesHelper.GetEmbeddedResourceScriptsFrom(assembly, testResourceNames);
            return resourceScripts;
        }

        private static void InstallTsqlt()
        {
            TsqltTestUtils.InstallTsqlt(GetLocalTestDatabaseConnectionString());
        }

        private static void ApplyPatches()
        {
            // here is where you would add any patches that need to be applied after your base database schema has been created.
            // we currently dp this using FluentMigrator (https://github.com/schambers/fluentmigrator)

            Migrator?.Migrate(ConnectionString);
        }

        private static void SetupTestDatabase()
        {
            var testDatabaseManager = CreateTestDatabaseManager();
            EnsureTestDatabaseExists(testDatabaseManager);
        }

        private static DatabaseManager CreateTestDatabaseManager()
        {
            var localTestDatabaseConnectionString = GetLocalTestDatabaseConnectionString();
            var testDatabaseManager = new DatabaseManager(localTestDatabaseConnectionString);
            return testDatabaseManager;
        }

        private static void EnsureTestDatabaseExists(DatabaseManager testDatabaseManager)
        {
            if (!testDatabaseManager.DatabaseExists())
            {
                // the code here could be changed to execute a sql script which represents a snapshot of the database at a point in time
                // the sql script could be an embedded resource and could be executed in a similar fashion to the tSQLt installation
                testDatabaseManager.CreateDatabase();
            }
        }

        private static void PrepareForTsqlt()
        {
            var localTestDatabaseConnectionString = GetLocalTestDatabaseConnectionString();
            TsqltTestUtils.SetClr(SqlOnOffOptions.On, localTestDatabaseConnectionString);
            TsqltTestUtils.SetTrustworthy(SqlOnOffOptions.On, localTestDatabaseConnectionString);
            TsqltTestUtils.RepairAuthorization(localTestDatabaseConnectionString);
        }

        public static string GetLocalTestDatabaseConnectionString()
        {
            return ConnectionString;
        }
    }
}