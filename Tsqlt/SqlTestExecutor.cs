using System.Data;
using System.Data.SqlClient;

namespace Tsqlt
{
    public class SqlTestExecutor : ISqlTestExecutor
    {
        public void RunTest(SqlConnection sqlConnection, string testName)
        {
            var sql = GetTestExecutionCommandSql(testName);

            using (var sqlCommand = new SqlCommand(sql, sqlConnection))
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        protected virtual string GetTestExecutionCommandSql(string testName)
        {
            return $"exec tSQLt.Run '{testName}'";
        }
    }
}