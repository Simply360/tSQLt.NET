using System;
using System.Collections.Generic;

namespace Tsqlt
{
    public class TestBase
    {
        public static void SetupTestFixture()
        {
            BootstrapForTests.BootstrapTestsDatabase();
        }

        public static void SetupTest()
        {
        }

        public static void RunTest(string fullTestName)
        {
            var localTestDatabaseConnectionString = BootstrapForTests.GetLocalTestDatabaseConnectionString();
            var testRunner = new MstestTsqltTestRunner(fullTestName, localTestDatabaseConnectionString);
            testRunner.Run();
        }

        // ReSharper disable UnusedMember.Global
        // This is used by the templates
        public static List<TsqltTest> GetTests()
        // ReSharper restore UnusedMember.Global
        {
            try
            {
                var localTestDatabaseConnectionString = BootstrapForTests.GetLocalTestDatabaseConnectionString();
                return TemplateUtils.GetTests(localTestDatabaseConnectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<TsqltTest>();
            }
        }
    }
}
