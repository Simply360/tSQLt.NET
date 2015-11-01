using System.Data.SqlClient;

namespace Tsqlt
{
    public interface ITsqltTestClassInstaller
    {
        void Install(ITsqltTestClass testClass, SqlConnection connection);
    }
}