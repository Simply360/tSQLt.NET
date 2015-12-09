using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TsqltNet.Tests
{
    [TestClass]
    public class TsqltTestClassTests
    {
        [TestMethod]
        public void NormalizedTestClassName_converts_spaces_to_underscores()
        {
            // Arrange
            const string testClassSchemaName = "One two three";

            // Act
            var testClass = new TsqltTestClass(testClassSchemaName, null);
            var normalizedTestClassName = testClass.NormalizedTestClassName;

            // Assert
            normalizedTestClassName.Should().Be("One_two_three");
        }

        [TestMethod]
        public void NormalizedTestClassName_converts_multiple_spaces_to_single_underscore()
        {
            // Arrange
            const string testClassSchemaName = "One          two   three";

            // Act
            var testClass = new TsqltTestClass(testClassSchemaName, null);
            var normalizedTestClassName = testClass.NormalizedTestClassName;

            // Assert
            normalizedTestClassName.Should().Be("One_two_three");
        }

        [TestMethod]
        public void NormalizedTestClassName_converts_dashes_to_underscores()
        {
            // Arrange
            const string testClassSchemaName = "One-two-three";

            // Act
            var testClass = new TsqltTestClass(testClassSchemaName, null);
            var normalizedTestClassName = testClass.NormalizedTestClassName;

            // Assert
            normalizedTestClassName.Should().Be("One_two_three");
        }

        [TestMethod]
        public void NormalizedTestClassName_converts_multiple_dashes_to_single_underscore()
        {
            // Arrange
            const string testClassSchemaName = "One----two-three";

            // Act
            var testClass = new TsqltTestClass(testClassSchemaName, null);
            var normalizedTestClassName = testClass.NormalizedTestClassName;

            // Assert
            normalizedTestClassName.Should().Be("One_two_three");
        }

        [TestMethod]
        public void NormalizedTestClassName_converts_mixed_spaces_and_dashes_to_underscores()
        {
            // Arrange
            const string testClassSchemaName = "One-two-three_four-five six seven-eight";

            // Act
            var testClass = new TsqltTestClass(testClassSchemaName, null);
            var normalizedTestClassName = testClass.NormalizedTestClassName;

            // Assert
            normalizedTestClassName.Should().Be("One_two_three_four_five_six_seven_eight");
        }

        [TestMethod]
        public void NormalizedTestClassName_converts_mixed_multiple_spaces_and_dashes_to_underscores()
        {
            // Arrange
            const string testClassSchemaName = "One- ---_two-three_four---    ____-five six seven-eight";

            // Act
            var testClass = new TsqltTestClass(testClassSchemaName, null);
            var normalizedTestClassName = testClass.NormalizedTestClassName;

            // Assert
            normalizedTestClassName.Should().Be("One_two_three_four_five_six_seven_eight");
        }

        [TestMethod]
        public void NormalizedTestClassName_strips_single_quotes()
        {
            // Arrange
            const string testClassSchemaName = "One's two's three's";

            // Act
            var testClass = new TsqltTestClass(testClassSchemaName, null);
            var normalizedTestClassName = testClass.NormalizedTestClassName;

            // Assert
            normalizedTestClassName.Should().Be("Ones_twos_threes");
        }

        [TestMethod]
        public void NormalizedTestClassName_strips_double_quotes()
        {
            // Arrange
            const string testClassSchemaName = "One\"s two\"s three\"s";

            // Act
            var testClass = new TsqltTestClass(testClassSchemaName, null);
            var normalizedTestClassName = testClass.NormalizedTestClassName;

            // Assert
            normalizedTestClassName.Should().Be("Ones_twos_threes");
        }

        [TestMethod]
        public void NormalizedTestClassName_strips_commas()
        {
            // Arrange
            const string testClassSchemaName = "One, two, three";

            // Act
            var testClass = new TsqltTestClass(testClassSchemaName, null);
            var normalizedTestClassName = testClass.NormalizedTestClassName;

            // Assert
            normalizedTestClassName.Should().Be("One_two_three");
        }

        [TestMethod]
        public void NormalizedTestClassName_strips_slashes()
        {
            // Arrange
            const string testClassSchemaName = "One/two/three";

            // Act
            var testClass = new TsqltTestClass(testClassSchemaName, null);
            var normalizedTestClassName = testClass.NormalizedTestClassName;

            // Assert
            normalizedTestClassName.Should().Be("Onetwothree");
        }
    }
}
