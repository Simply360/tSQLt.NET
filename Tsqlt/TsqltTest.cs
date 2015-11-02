namespace Tsqlt
{
    public class TsqltTest : ITsqltTest
    {
        public string Name { get; }
        public string ProcedureName => $"test {Name}";
        public string NormalizedTestMethodName => Name.Replace(" ", "_").Replace("-", "_");
        public string TestCaseBody { get; }

        public TsqltTest(string name, string testCaseBody)
        {
            Name = name;
            TestCaseBody = testCaseBody;
        }
    }
}