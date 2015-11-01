using System.Reflection;

namespace Tsqlt
{
    public static class TestEnvironmentFactory
    {
        public static ITestEnvironment Create(string connectionString, Assembly testAssembly, string testResourcePath, IDbMigrator dbMigrator = null)
        {
            // Poor-man's Dependency Injection
            var embeddedTextResourceReader = new EmbeddedTextResourceReader();
            var sqlBatchExtractor = new SqlBatchExtractor();
            var tsqltInstaller = new EmbeddedResourceTsqltInstaller(embeddedTextResourceReader, sqlBatchExtractor);
            var testClassInstaller = new TsqltTestClassInstaller(embeddedTextResourceReader);
            var testClassDiscoverer = new AssemblyResourceTestClassDiscoverer(testAssembly, testResourcePath, embeddedTextResourceReader);
            var bootstrapper = new TestEnvironmentBootstrapper(tsqltInstaller, dbMigrator ?? new DefaultDbMigrator(), testClassDiscoverer, testClassInstaller);
            var outputMessageWriter = new DebugTestOutputMessageWriter();
            var sqlTestExecutor = new MessageWritingSqlTestExecutor(outputMessageWriter, new SqlTestExecutor());
            return new BootstrappedTestEnvironment(connectionString, bootstrapper, sqlTestExecutor);
        }
    }
}