﻿using System.Data.SqlClient;

namespace TsqltNet
{
    public class SqlHelper
    {
        public static object ExecuteScalar(string sql, string connectionString)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    return sqlCommand.ExecuteScalar();
                }
            }
        }

        public static object ExecuteNonQuery(string sql, string connectionString)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    return sqlCommand.ExecuteScalar();
                }
            }
        }
    }
}
