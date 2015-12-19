namespace TsqltNet
{
    /// <summary>
    /// Service for bootstrapping a tSQLt environment
    /// </summary>
    public interface ITestEnvironmentBootstrapper
    {
        /// <summary>
        /// Bootstraps the environment using the given SQL connection
        /// </summary>
        /// <returns>A connection string for the test database</returns>
        string BootstrapEnvironment();
    }
}