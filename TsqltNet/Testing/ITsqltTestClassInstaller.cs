using System.Data.SqlClient;

namespace TsqltNet.Testing
{
    public interface ITsqltTestClassInstaller
    {
        IInstalledTestClass Install(ITsqltTestClass testClass, SqlConnection connection);
    }
}