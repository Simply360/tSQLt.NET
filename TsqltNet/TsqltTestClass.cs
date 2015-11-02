namespace TsqltNet
{
    public sealed class TsqltTestClass : ITsqltTestClass
    {
        public string TestClassSchemaName { get; }
        public ITsqltTest[] Tests { get; }
        public string NormalizedTestClassName => TestClassSchemaName.Replace(" ", "_").Replace("-", "_");

        public TsqltTestClass(string testClassSchemaName, ITsqltTest[] tests)
        {
            TestClassSchemaName = testClassSchemaName;
            Tests = tests;
        }
    }
}