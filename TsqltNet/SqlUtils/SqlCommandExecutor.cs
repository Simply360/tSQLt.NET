using System.Data.SqlClient;

namespace TsqltNet.SqlUtils
{
    public class SqlCommandExecutor : ISqlCommandExecutor
    {
        public void ExecuteNonQuery(SqlConnection connection, string commandText)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.ExecuteNonQuery();
        }

        public object ExecuteScalar(SqlConnection connection, string commandText)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            return command.ExecuteScalar();
        }
    }
}