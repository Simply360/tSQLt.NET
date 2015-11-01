namespace Tsqlt
{
    /// <summary>
    /// Service for bootstrapping a tSQLt environment
    /// </summary>
    public interface ITestEnvironmentBootstrapper
    {
        /// <summary>
        /// Bootstraps the environment using the given SQL connection
        /// </summary>
        /// <param name="connectionString"></param>
        void BootstrapEnvironment(string connectionString);
    }
}