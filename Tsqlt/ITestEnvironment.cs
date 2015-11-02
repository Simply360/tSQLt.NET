namespace Tsqlt
{
    public interface ITestEnvironment
    {
        /// <summary>
        /// Runs the test with the given name in the environment
        /// </summary>
        /// <param name="testClassName">The name of the test class the test case belongs to</param>
        /// <param name="testName">The name of the test case</param>
        void RunTest(string testClassName, string testName);
    }
}