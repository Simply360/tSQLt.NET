using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TsqltNet.Tests
{
    [TestClass]
    public class MessageWritingSqlTestExecutorTests
    {
        [TestMethod]
        [Ignore]
        public void Runs_inner_executor_method()
        {
            // Arrange
            const string testName = "foo.bar";
            var sqlConnection = new SqlConnection();
            var innerExecutor = new Mock<ISqlTestExecutor>(MockBehavior.Strict);
            var testOutputMessageWriter = new Mock<ITestOutputMessageWriter>(MockBehavior.Strict);
            var sut = new MessageWritingSqlTestExecutor(testOutputMessageWriter.Object, innerExecutor.Object);

            // Act
            sut.RunTest(sqlConnection, testName);

            // Assert
            Assert.Fail(); // TODO
        }
    }
}
