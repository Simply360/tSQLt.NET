using System;

namespace TsqltNet
{
    public class BootstrappedTestEnvironment : ITestEnvironment
    {
        private readonly ITestEnvironmentBootstrapper _bootstrapper;
        private readonly Func<string, ITestRunner> _testRunnerFactory;
        private readonly object _bootstrapLock;
        private bool IsBootstrapped { get; set; }
        private string TestDbConnectionString { get; set; }

        public BootstrappedTestEnvironment(ITestEnvironmentBootstrapper bootstrapper,
            Func<string, ITestRunner> testRunnerFactory)
        {
            if (bootstrapper == null) throw new ArgumentNullException(nameof(bootstrapper));
            if (testRunnerFactory == null) throw new ArgumentNullException(nameof(testRunnerFactory));
            _bootstrapper = bootstrapper;
            _testRunnerFactory = testRunnerFactory;
            _bootstrapLock = new object();
        }

        public ITestRunner GetTestRunner()
        {
            lock (_bootstrapLock)
            {
                if (!IsBootstrapped)
                {
                    TestDbConnectionString = _bootstrapper.BootstrapEnvironment();
                    IsBootstrapped = true;
                }
            }

            return _testRunnerFactory(TestDbConnectionString);
        }
    }
}