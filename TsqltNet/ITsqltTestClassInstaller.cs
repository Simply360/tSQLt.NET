using System.Data.SqlClient;

namespace TsqltNet
{
    public interface ITsqltTestClassInstaller
    {
        void Install(ITsqltTestClass testClass, SqlConnection connection);
    }
}