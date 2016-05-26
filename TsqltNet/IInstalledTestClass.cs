using System.Data.SqlClient;
using TsqltNet.Testing;

namespace TsqltNet
{
    public interface IInstalledTestClass
    {
        IInstalledTest GetInstalledTest(ITsqltTest test, SqlConnection connection);
    }
}