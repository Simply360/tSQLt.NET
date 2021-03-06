﻿using System.Data.SqlClient;

namespace TsqltNet
{
    public class MessageWritingSqlTestExecutor : ISqlTestExecutor
    {
        private readonly ITestOutputMessageWriter _outputMessageWriter;
        private readonly ISqlTestExecutor _innerExecutor;

        public MessageWritingSqlTestExecutor(ITestOutputMessageWriter outputMessageWriter, ISqlTestExecutor innerExecutor)
        {
            _outputMessageWriter = outputMessageWriter;
            _innerExecutor = innerExecutor;
        }

        public void RunTest(SqlConnection sqlConnection, IInstalledTest installedTest)
        {
            SqlInfoMessageEventHandler onInfoMessage = (sender, e) =>
            {
                _outputMessageWriter.WriteLine(e.Message);
            };
            sqlConnection.InfoMessage += onInfoMessage;

            try
            {
                _innerExecutor.RunTest(sqlConnection, installedTest);
            }
            finally
            {
                sqlConnection.InfoMessage -= onInfoMessage;
            }
        }
    }
}