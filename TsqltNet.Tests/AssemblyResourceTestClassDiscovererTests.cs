using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TsqltNet.Tests
{
    [TestClass]
    public class AssemblyResourceTestClassDiscovererTests
    {
        [TestMethod]
        public void Finds_tests()
        {
            // Arrange
            var reader = new EmbeddedTextResourceReader();
            var sut = new AssemblyResourceTestClassDiscoverer(Assembly.GetExecutingAssembly(), "TsqltNet.Tests.Fixtures.AssemblyResourceTestClassDiscoverer", reader);

            // Act
            var testClasses = sut.DiscoverTests();

            // Assert
            testClasses.ShouldAllBeEquivalentTo(
                new[]
                {
                    new TsqltTestClass("TestClass1", new ITsqltTest[]
                    {
                        new TsqltTest("a test with dashes-and spaces", "TEST CASE CONTENT 1"),
                        new TsqltTest("a test with spaces", "TEST CASE CONTENT 2"),
                    }),
                    new TsqltTestClass("TestClass2", new ITsqltTest[]
                    {
                        new TsqltTest("a test with dashes-and spaces", "TEST CASE CONTENT 3"),
                        new TsqltTest("a test with spaces", "TEST CASE CONTENT 4"),
                        new TsqltTest("AnotherTest", "TEST CASE CONTENT 5")
                    })
                });
        }
    }
}
