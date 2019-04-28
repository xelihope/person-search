using NUnit.Framework;
using PersonSearch.Helpers;

namespace PersonSearch.Tests.Helpers
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("", true)]
        [TestCase(null, true)]
        [TestCase("   \t", true)]
        [TestCase("\"", false)]
        [TestCase("this is a string 1283u14801890", false)]
        public void IsNullOrBlankTest(string value, bool expected)
        {
            // Act
            var result = value.IsNullOrBlank();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
