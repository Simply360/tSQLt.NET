namespace Tsqlt
{
    public class TestResult
    {
        public int Id { get; }
        public string Class { get; }
        public string TestCase { get; }
        public string Name { get; }
        public string TranName { get; }
        public string Result { get; }
        public string Msg { get; }

        public TestResult(int id, string @class, string testCase, string name, string tranName, string result, string msg)
        {
            Id = id;
            Class = @class;
            TestCase = testCase;
            Name = name;
            TranName = tranName;
            Result = result;
            Msg = msg;
        }
    }
}
