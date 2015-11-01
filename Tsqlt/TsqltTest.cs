namespace Tsqlt
{
    public class TsqltTest : ITsqltTest
    {
        public string Name { get; }
        public string TestCaseBody { get; }

        public TsqltTest(string name, string testCaseBody)
        {
            Name = name;
            TestCaseBody = testCaseBody;
        }
    }
}