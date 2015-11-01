using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Tsqlt
{
    public class TemplateUtils
    {
        public static List<OldTsqltTest> GetTests(string connectionString)
        {
            const string sql = "SELECT schemaid, testclassname, objectid, name FROM tSQLt.Tests ORDER BY TestClassName , Name ASC";
            var tests = new List<OldTsqltTest>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tests.Add(new OldTsqltTest(
                                reader.GetInt32(reader.GetOrdinal("SchemaId")),
                                reader.GetString(reader.GetOrdinal("TestClassName")),
                                reader.GetInt32(reader.GetOrdinal("ObjectId")),
                                reader.GetString(reader.GetOrdinal("Name"))));
                        }
                    }
                }
            }

            return tests;
        }
    }
}