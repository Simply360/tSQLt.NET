using System.Data.SqlClient;

namespace TsqltNet
{
    public interface ITsqltInstaller
    {
        void Install(SqlConnection connection);
    }
}