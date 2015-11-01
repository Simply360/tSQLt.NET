namespace Tsqlt
{
    public interface ITestEnvironment
    {
        /// <summary>
        /// Runs the test with the given name in the environment
        /// </summary>
        /// <param name="testName">The name of the test</param>
        void RunTest(string testName);
    }
}