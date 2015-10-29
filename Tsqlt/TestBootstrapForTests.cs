using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tsqlt
{
    [TestClass]
    public class TestBootstrapForTests
    {
        [TestMethod]
        public void BootstrapForTests_ShouldRun()
        {
            BootstrapForTests.BootstrapTestsDatabase();
        }
    }
}
