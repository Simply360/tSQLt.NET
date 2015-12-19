using System.Data.SqlClient;

namespace TsqltNet.Testing
{
    public interface ITsqltTestClassInstaller
    {
        void Install(ITsqltTestClass testClass, SqlConnection connection);
    }
}