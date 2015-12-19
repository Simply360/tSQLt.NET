using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TsqltNet.Tests
{
    [TestClass]
    public class BootstrappedTestEnvironmentTests
    {
        [TestMethod]
        public void GetTestRunner_calls_bootstrapper_BootstrapEnvironment()
        {
            // Arrange
            var connectionString = "SOME_CONNECTION_STRING";
            var bootstrapper = new Mock<ITestEnvironmentBootstrapper>(MockBehavior.Strict);
            bootstrapper.Setup(b => b.BootstrapEnvironment()).Returns(connectionString).Verifiable();

            Func<string, ITestRunner> testRunnerFactory = actualConnectionString =>
            {
                actualConnectionString.Should().Be(connectionString);
                return new Mock<ITestRunner>().Object;
            };
            var sut = new BootstrappedTestEnvironment(bootstrapper.Object, testRunnerFactory);

            // Act
            sut.GetTestRunner();
            sut.GetTestRunner();

            // Assert
            bootstrapper.Verify(b => b.BootstrapEnvironment(), Times.Once);
        }
    }
}
