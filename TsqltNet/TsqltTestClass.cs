using System;

namespace TsqltNet
{
    public sealed class TsqltTestClass : ITsqltTestClass
    {
        public string TestClassSchemaName { get; }
        public ITsqltTest[] Tests { get; }

        public string NormalizedTestClassName => IdentifierFormatter.FormatString(TestClassSchemaName);

        private IIdentifierFormatter _identifierFormatter;

        public IIdentifierFormatter IdentifierFormatter
        {
            get { return _identifierFormatter ?? (_identifierFormatter = new IdentifierFormatter()); }
            set
            {
                if (_identifierFormatter != null) throw new ArgumentException("Setting the identifier formatter more than once is not supported.");
                _identifierFormatter = value;
            }
        }

        public TsqltTestClass(string testClassSchemaName, ITsqltTest[] tests)
        {
            TestClassSchemaName = testClassSchemaName;
            Tests = tests;
        }
    }
}