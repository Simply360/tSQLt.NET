namespace Tsqlt
{
    public class TsqltTest : ITsqltTest
    {
        public string ProcedureName { get; }
        public string NormalizedTestMethodName => ProcedureName.Replace(" ", "_").Replace("-", "_");
        public string TestCaseBody { get; }

        public TsqltTest(string procedureName, string testCaseBody)
        {
            ProcedureName = procedureName;
            TestCaseBody = testCaseBody;
        }
    }
}