using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using TsqltNet.Testing;

namespace TsqltNet
{
    public class InstalledTestClass : IInstalledTestClass
    {
        private const string TestCaseBoilerplateEmbeddedResourcePath = "TsqltNet.TestCaseBoilerplate.sql";
        private string _testCaseBoilerplate;

        private readonly IEmbeddedTextResourceReader _embeddedTextResourceReader;
        private readonly string _testClassSchemaName;

        public InstalledTestClass(IEmbeddedTextResourceReader embeddedTextResourceReader, string testClassSchemaName)
        {
            _embeddedTextResourceReader = embeddedTextResourceReader;
            _testClassSchemaName = testClassSchemaName;
        }

        public IInstalledTest GetInstalledTest(ITsqltTest test, SqlConnection connection)
        {
            CreateTest(_testClassSchemaName, test.ProcedureName, test.TestCaseBody, connection);
            var fullTestName = $"[{_testClassSchemaName}].[{test.ProcedureName}]";
            return new InstalledTest(fullTestName);
        }

        protected virtual void CreateTest(string testClassSchemaName, string procedureName, string testCaseBody, SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            Debug.WriteLine($"Installing test {testClassSchemaName}.{procedureName}");
            Debug.WriteLine("-----");
            var commandText = TestCaseBoilerplate
                .Replace("{{testClassSchemaName}}", testClassSchemaName)
                .Replace("{{procedureName}}", procedureName)
                .Replace("{{escapedTestClassSchemaName}}", testClassSchemaName.Replace("'", "''"))
                .Replace("{{escapedProcedureName}}", procedureName.Replace("'", "''"))
                .Replace("{{testCaseBody}}", testCaseBody);
            Debug.WriteLine(commandText);

            using (var command = new SqlCommand(commandText, connection))
            {
                command.ExecuteNonQuery();
            }
            Debug.WriteLine("-----");
            Debug.WriteLine($"Successfully installed test {testClassSchemaName}.{procedureName}");
        }

        private string TestCaseBoilerplate =>
            _testCaseBoilerplate ??
            (_testCaseBoilerplate =
                _embeddedTextResourceReader.GetResourceContents(Assembly.GetExecutingAssembly(),
                    TestCaseBoilerplateEmbeddedResourcePath));
    }
}