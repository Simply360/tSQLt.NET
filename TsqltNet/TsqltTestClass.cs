using System.Text.RegularExpressions;

namespace TsqltNet
{
    public sealed class TsqltTestClass : ITsqltTestClass
    {
        public string TestClassSchemaName { get; }
        public ITsqltTest[] Tests { get; }

        public string NormalizedTestClassName =>
            Regex.Replace(TestClassNameWithoutInvalidCharacters, @"[\s_-]+", "_");

        private string TestClassNameWithoutInvalidCharacters =>
            Regex.Replace(TestClassSchemaName, @"[^\w\s_-]", "");

        public TsqltTestClass(string testClassSchemaName, ITsqltTest[] tests)
        {
            TestClassSchemaName = testClassSchemaName;
            Tests = tests;
        }
    }
}