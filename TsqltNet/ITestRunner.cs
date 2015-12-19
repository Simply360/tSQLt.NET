namespace TsqltNet
{
    public interface ITestRunner
    {
        /// <summary>
        /// Runs the test with the given name
        /// </summary>
        /// <param name="testClassSchemaName">The name of the test class the test case belongs to</param>
        /// <param name="testProcedureName">The name of the stored procedure corresponding to the test case</param>
        void RunTest(string testClassSchemaName, string testProcedureName);
    }
}