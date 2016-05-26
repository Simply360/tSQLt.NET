using System.Data;
using System.Data.SqlClient;

namespace TsqltNet
{
    public class SqlTestExecutor : ISqlTestExecutor
    {
        public void RunTest(SqlConnection sqlConnection, IInstalledTest installedTest)
        {
            using (var sqlCommand = CreateCommand(installedTest.FullTestName, sqlConnection))
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        protected virtual SqlCommand CreateCommand(string testName, SqlConnection sqlConnection)
        {
            const string sql = "exec tSQLt.Run @TestName";
            var command = new SqlCommand(sql, sqlConnection);

            var param = new SqlParameter("TestName", SqlDbType.NVarChar) {Value = testName};
            command.Parameters.Add(param);

            return command;
        }
    }
}