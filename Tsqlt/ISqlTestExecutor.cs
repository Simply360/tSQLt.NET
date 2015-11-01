using System.Data.SqlClient;

namespace Tsqlt
{
    public interface ISqlTestExecutor
    {
        /// <summary>
        /// Runs the test with the given name
        /// </summary>
        /// <param name="sqlConnection">The connection to run the test in</param>
        /// <param name="testName">The schema-qualified name of the test to run</param>
        void RunTest(SqlConnection sqlConnection, string testName);
    }
}