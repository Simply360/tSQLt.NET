using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TsqltNet.Testing;

namespace TsqltNet
{
    public class TestInstaller : ITestInstaller
    {
        private readonly ITestClassDiscoverer _testClassDiscoverer;
        private readonly ITsqltTestClassInstaller _tsqltTestClassInstaller;
        private readonly IDictionary<ITsqltTestClass, IInstalledTestClass> _installedTestClasses =
            new Dictionary<ITsqltTestClass, IInstalledTestClass>();
        private IDictionary<string, ITsqltTestClass> _testClasses;

        public TestInstaller(ITestClassDiscoverer testClassDiscoverer,
            ITsqltTestClassInstaller tsqltTestClassInstaller)
        {
            _testClassDiscoverer = testClassDiscoverer;
            _tsqltTestClassInstaller = tsqltTestClassInstaller;
        }

        public IInstalledTest InstallTest(string testClassSchemaName, string testProcedureName, SqlConnection connection)
        {
            EnsureTestsAreDiscovered();
            
            ITsqltTestClass tsqltTestClass;
            if (!_testClasses.TryGetValue(testClassSchemaName, out tsqltTestClass))
                throw new InvalidOperationException($"Could not find the test class with schema name {testClassSchemaName}");

            var test = tsqltTestClass.GetTestByProcedureName(testProcedureName);
            if (test == null)
                throw new InvalidOperationException(
                    $"Could not find the test with schema name {testClassSchemaName} and procedure name {testProcedureName}");

            IInstalledTestClass installedTestClass;
            if (!_installedTestClasses.TryGetValue(tsqltTestClass, out installedTestClass))
            {
                installedTestClass = _tsqltTestClassInstaller.Install(tsqltTestClass, connection);
                _installedTestClasses[tsqltTestClass] = installedTestClass;
            }

            return installedTestClass.GetInstalledTest(test, connection);
        }

        private void EnsureTestsAreDiscovered()
        {
            if (_testClasses != null) return;
            var allTestClasses = _testClassDiscoverer.DiscoverTests();
            _testClasses = allTestClasses.ToDictionary(t => t.TestClassSchemaName);
        }
    }
}