using System.Data;
using System.Data.SqlClient;
using TsqltNet.Testing;

namespace TsqltNet
{
    public class TsqltTestClassInstaller : ITsqltTestClassInstaller
    {
        private readonly IEmbeddedTextResourceReader _embeddedTextResourceReader;

        public TsqltTestClassInstaller(IEmbeddedTextResourceReader embeddedTextResourceReader)
        {
            _embeddedTextResourceReader = embeddedTextResourceReader;
        }

        public IInstalledTestClass Install(ITsqltTestClass testClass, SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            DropClass(testClass.TestClassSchemaName, connection);
            CreateClass(testClass.TestClassSchemaName, connection);

            return new InstalledTestClass(_embeddedTextResourceReader, testClass.TestClassSchemaName);
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
    }
}