using System.Data.SqlClient;

namespace TsqltNet.SqlUtils
{
    public interface ISqlCommandExecutor
    {
        void ExecuteNonQuery(SqlConnection connection, string commandText);

        object ExecuteScalar(SqlConnection connection, string commandText);
    }
}
