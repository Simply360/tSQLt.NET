using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Tsqlt
{
    public class TsqltTestClassInstaller : ITsqltTestClassInstaller
    {
        private const string TestCaseBoilerplateEmbeddedResourcePath = "Tsqlt.TestCaseBoilerplate.sql";
        private readonly IEmbeddedTextResourceReader _embeddedTextResourceReader;
        private string _testCaseBoilerplate;

        public TsqltTestClassInstaller(IEmbeddedTextResourceReader embeddedTextResourceReader)
        {
            _embeddedTextResourceReader = embeddedTextResourceReader;
        }

        public void Install(ITsqltTestClass testClass, SqlConnection connection)
        {
            DropClass(testClass.Name, connection);
            CreateClass(testClass.Name, connection);

            foreach (var test in testClass.Tests)
            {
                CreateTest(testClass.Name, test.Name, test.TestCaseBody, connection);
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

        protected virtual void CreateTest(string testClassName, string testName, string testCaseBody, SqlConnection connection)
        {
            var commandText = TestCaseBoilerplate
                .Replace("{{testClass}}", testClassName)
                .Replace("{{testName}}", testName)
                .Replace("{{testCaseBody}}", testCaseBody);

            using (var command = new SqlCommand(commandText, connection))
            {
                command.Parameters.Add(new SqlParameter(testClassName, SqlDbType.NVarChar, 256));
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