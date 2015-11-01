namespace Tsqlt
{
    public sealed class TsqltTestClass : ITsqltTestClass
    {
        public string TestClassName { get; }
        public ITsqltTest[] Tests { get; }
        public string NormalizedTestClassName => TestClassName.Replace(" ", "_").Replace("-", "_");

        public TsqltTestClass(string testClassName, ITsqltTest[] tests)
        {
            TestClassName = testClassName;
            Tests = tests;
        }
    }
}