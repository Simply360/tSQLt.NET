using System.Data.SqlClient;

namespace TsqltNet
{
    public interface ISqlTestExecutor
    {
        /// <summary>
        /// Runs the test with the given name
        /// </summary>
        /// <param name="sqlConnection">The connection to run the test in</param>
        /// <param name="installedTest">The installed test to run</param>
        void RunTest(SqlConnection sqlConnection, IInstalledTest installedTest);
    }
}