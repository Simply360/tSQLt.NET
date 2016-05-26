using System.Data.SqlClient;

namespace TsqltNet
{
    public interface ITestInstaller
    {
        IInstalledTest InstallTest(string testClassSchemaName, string testProcedureName, SqlConnection connection);
    }
}