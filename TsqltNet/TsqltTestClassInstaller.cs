using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using TsqltNet.Testing;

namespace TsqltNet
{
    public class TsqltTestClassInstaller : ITsqltTestClassInstaller
    {
        private const string TestCaseBoilerplateEmbeddedResourcePath = "TsqltNet.TestCaseBoilerplate.sql";
        private readonly IEmbeddedTextResourceReader _embeddedTextResourceReader;
        private string _testCaseBoilerplate;

        public TsqltTestClassInstaller(IEmbeddedTextResourceReader embeddedTextResourceReader)
        {
            _embeddedTextResourceReader = embeddedTextResourceReader;
        }

        public void Install(ITsqltTestClass testClass, SqlConnection connection)
        {
            DropClass(testClass.TestClassSchemaName, connection);
            CreateClass(testClass.TestClassSchemaName, connection);

            foreach (var test in testClass.Tests)
            {
                CreateTest(testClass.TestClassSchemaName, test.ProcedureName, test.TestCaseBody, connection);
            }
        }

        protected virtual void DropClass(string testClassName, SqlConnection connection)
        {
            var commandText = $"EXEC tSQLt.DropClass {testClassName}";
            using (var command = new SqlCommand(commandText, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        protected virtual void CreateClass(string testClassName, SqlConnection connection)
        {
            var commandText = $"EXEC tSQLt.NewTestClass {testClassName}";
            using (var command = new SqlCommand(commandText, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        protected virtual void CreateTest(string testClassSchemaName, string procedureName, string testCaseBody, SqlConnection connection)
        {
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