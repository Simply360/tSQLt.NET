using System;

namespace TsqltNet
{
    public class TsqltTest : ITsqltTest
    {
        public string Name { get; }
        public string ProcedureName => $"test {Name}";
        public string TestCaseBody { get; }

        public string NormalizedTestMethodName => IdentifierFormatter.FormatString(Name);

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

        public TsqltTest(string name, string testCaseBody)
        {
            Name = name;
            TestCaseBody = testCaseBody;
        }
    }
}