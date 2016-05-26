using System;
using System.Collections.Generic;
using System.Linq;

namespace TsqltNet.Testing
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

        private readonly Lazy<IDictionary<string, ITsqltTest>> _testDict;

        public TsqltTestClass(string testClassSchemaName, ITsqltTest[] tests)
        {
            TestClassSchemaName = testClassSchemaName;
            Tests = tests;

            _testDict = new Lazy<IDictionary<string, ITsqltTest>>(() =>
            {
                return tests.ToDictionary(t => t.ProcedureName);
            });
        }

        public ITsqltTest GetTestByProcedureName(string testProcedureName)
        {
            ITsqltTest test;
            return _testDict.Value.TryGetValue(testProcedureName, out test) ? test : null;
        }
    }
}