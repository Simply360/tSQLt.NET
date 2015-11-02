using System;
using System.Linq;
using System.Reflection;

namespace TsqltNet
{
    public class AssemblyResourceTestClassDiscoverer : ITestClassDiscoverer
    {
        private readonly Assembly _testAssembly;
        private readonly string _testsRootResourcePath;
        private readonly IEmbeddedTextResourceReader _embeddedTextResourceReader;

        public AssemblyResourceTestClassDiscoverer(Assembly testAssembly, string testsRootResourcePath,
            IEmbeddedTextResourceReader embeddedTextResourceReader)
        {
            if (testAssembly == null) throw new ArgumentNullException(nameof(testAssembly));
            if (testsRootResourcePath == null) throw new ArgumentNullException(nameof(testsRootResourcePath));

            _testAssembly = testAssembly;
            _testsRootResourcePath = testsRootResourcePath;
            _embeddedTextResourceReader = embeddedTextResourceReader;
        }

        public AssemblyResourceTestClassDiscoverer(Assembly testAssembly, string testsRootResourcePath)
            : this(testAssembly, testsRootResourcePath, new EmbeddedTextResourceReader())
        {
            
        }

        public ITsqltTestClass[] DiscoverTests()
        {
            var embeddedResources = _testAssembly.GetManifestResourceNames();
            var resourcesUndernathRootPath = embeddedResources.Where(r => r.StartsWith(NormalizedTestsRootResourcePath));

            var sqlFiles = resourcesUndernathRootPath.Where(r => r.EndsWith(".sql"));
            
            // Get the resource names relative to NormalizedTestsRootResourcePath
            var resourceNameSplits = sqlFiles.Select(r => r.Substring(NormalizedTestsRootResourcePath.Length).Split('.'));

            // Only tests in the format {testClassName}.{testCaseName}.sql are supported
            var validSplits = resourceNameSplits.Where(r => r.Length == 3);
            var testClassNames = validSplits.GroupBy(r => r[0], r => r[1]);

            return testClassNames
                .Select(tcn => (ITsqltTestClass) new TsqltTestClass(tcn.Key, tcn.Select(n => GetTest(tcn.Key, n)).ToArray()))
                .ToArray();
        }

        private ITsqltTest GetTest(string testClassName, string testName)
        {
            var testResourcePath = $"{NormalizedTestsRootResourcePath}{testClassName}.{testName}.sql";
            var testCaseBody = _embeddedTextResourceReader.GetResourceContents(_testAssembly, testResourcePath);
            return new TsqltTest(testName, testCaseBody);
        }

        private string NormalizedTestsRootResourcePath
            => _testsRootResourcePath.EndsWith(".") ? _testsRootResourcePath : $"{_testsRootResourcePath}.";
    }
}