namespace Tsqlt
{
    public sealed class TsqltTestClass : ITsqltTestClass
    {
        public string Name { get; }
        public ITsqltTest[] Tests { get; }

        public TsqltTestClass(string name, ITsqltTest[] tests)
        {
            Name = name;
            Tests = tests;
        }
    }
}