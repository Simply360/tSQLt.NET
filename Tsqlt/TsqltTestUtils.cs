using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tsqlt
{
    public enum SqlOnOffOptions
    {
        On,
        Off
    }

    public class TsqltTestUtils
    {
        public static void SetClr(SqlOnOffOptions onOffOption, string connectionString)
        {
            var sql = "EXEC sp_configure 'clr enabled', 1; " + Environment.NewLine +
                      "RECONFIGURE; ";

            ExecuteSql(sql, connectionString);
        }

        public static void SetTrustworthy(SqlOnOffOptions onOffOption, string connectionString)
        {
            var sql = @"DECLARE @cmd NVARCHAR(MAX); " + Environment.NewLine +
                      "SET @cmd='ALTER DATABASE ' + QUOTENAME(DB_NAME()) + ' SET TRUSTWORTHY ON;'; " +
                      Environment.NewLine +
                      "EXEC(@cmd); ";

            ExecuteSql(sql, connectionString);
        }

        public static void RepairAuthorization(string connectionString)
        {
            var sql = @"DECLARE @cmd NVARCHAR(MAX); " + Environment.NewLine +
                      "SET @cmd='ALTER AUTHORIZATION ON Database::' + QUOTENAME(DB_NAME()) + ' TO [sa]'; " +
                      Environment.NewLine +
                      "EXEC(@cmd); ";

            ExecuteSql(sql, connectionString);
        }

        public static void InstallTsqlt(string connectionString)
        {
            var resourcesHelper = new ResourcesHelper();
            var sql = resourcesHelper.GetEmbeddedResource("Tsqlt", "tSQLt.class.sql");
            ExecuteNonQuerySmo(sql, connectionString);
        }

        public static void ApplyEmbeddedResourceScripts(IEnumerable<string> embeddedResourceScripts, string connectionString)
        {
            if (embeddedResourceScripts == null) return;
            foreach (var fullyQualifiedScriptName in embeddedResourceScripts)
            {
                ExecuteNonQuerySmo(fullyQualifiedScriptName, connectionString);
            }
        }

        private static void ExecuteNonQuerySmo(string sql, string connectionString)
        {
            var statements = SplitSqlStatements(sql);
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (var statement in statements)
                {
                    using (var cmd = new SqlCommand(statement, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static IEnumerable<string> SplitSqlStatements(string sqlScript)
        {
            // Split by "GO" statements
            var statements = Regex.Split(
                    sqlScript,
                    @"^\s*GO\s* ($ | \-\- .*$)",
                    RegexOptions.Multiline |
                    RegexOptions.IgnorePatternWhitespace |
                    RegexOptions.IgnoreCase);

            // Remove empties, trim, and return
            return statements
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\r', '\n'));
        }

        private static void ExecuteSql(string sql, string connectionString)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<TestResult> GetTestResult(string name, string connectionString)
        {
            var sql = $"select id, class, testcase, name, tranname, result, msg from tSQLt.TestResult where name='{name}'";
            var tests = new List<TestResult>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tests.Add(new TestResult
                                (
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Class")),
                                reader.GetString(reader.GetOrdinal("TestCase")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetString(reader.GetOrdinal("TranName")),
                                reader.GetString(reader.GetOrdinal("Result")),
                                reader.GetString(reader.GetOrdinal("Msg"))
                                ));
                        }
                    }
                }
            }

            return tests;
        }
    }
}
