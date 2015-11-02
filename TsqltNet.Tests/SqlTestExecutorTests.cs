using System;
using System.Data.SqlClient;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TsqltNet.Tests
{
    [TestClass]
    [Ignore]
    public class SqlTestExecutorTests
    {
        [TestMethod]
        public void Asserts_when_test_does_not_exist()
        {
            // Arrange
            var sqlConnectionWrapper = TestUtils.GetTestDbConnection();
            var sut = new SqlTestExecutor();

            // Act
            Action action = () =>
            {
                sut.RunTest(sqlConnectionWrapper, "foo.bar");
            };
            
            // Assert
            action.ShouldThrow<InvalidOperationException>().WithMessage("The test \"foo.bar\" does not exist.");
        }
    }
}
