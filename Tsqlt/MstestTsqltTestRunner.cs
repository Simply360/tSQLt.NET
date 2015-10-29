using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tsqlt
{
    public class MstestTsqltTestRunner
    {
        public string TestFullName { get; }
        private readonly string _connectionString;

        public MstestTsqltTestRunner(string testFullName, string connectionString)
        {
            if (testFullName == null) throw new ArgumentNullException(nameof(testFullName));
            TestFullName = testFullName;
            _connectionString = connectionString;
        }

        public void Run()
        {
            ExecuteSqlTest();
            AssertOnResult();
        }

        private void AssertOnResult()
        {
            var testResult = TsqltTestUtils.GetTestResult(TestFullName, _connectionString).FirstOrDefault();

            if (testResult == null)
                Assert.Fail("Could not find test");
            if (testResult.Result.ToLower() != "success")
                Assert.Fail(testResult.Msg);
        }

        private void ExecuteSqlTest()
        {
            var sql = $"exec tSQLt.Run '{TestFullName}'";
            try
            {
                SqlHelper.ExecuteNonQuery(sql, _connectionString);
            }
            /* Leave this here to suppress errors in Nunit output */
            catch (Exception)
            {
            }
        }
    }
}
