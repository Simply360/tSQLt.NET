using System.Data;
using System.Data.SqlClient;
using System.Reflection;

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
            var commandText = TestCaseBoilerplate
                .Replace("{{testClassSchemaName}}", testClassSchemaName)
                .Replace("{{procedureName}}", procedureName)
                .Replace("{{testCaseBody}}", testCaseBody);

            using (var command = new SqlCommand(commandText, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private string TestCaseBoilerplate =>
            _testCaseBoilerplate ??
            (_testCaseBoilerplate =
                _embeddedTextResourceReader.GetResourceContents(Assembly.GetExecutingAssembly(),
                    TestCaseBoilerplateEmbeddedResourcePath));
    }
}