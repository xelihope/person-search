using NUnit.Framework;
using PersonSearch.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using PersonSearch.Helpers;

namespace PersonSearch.Tests.Services
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        private enum TestEnum
        {
            Test1,
            Test2 = 100,
            [Display(Name = "name of 3")]
            Test3,
            [Display(Name = nameof(HobbyType.Art), ResourceType = typeof(Resources.HobbyType))]
            Test4
        }

        [TestCase(TestEnum.Test1, "Test1")]
        [TestCase(TestEnum.Test2, "Test2")]
        [TestCase(TestEnum.Test3, "name of 3")]
        [TestCase(TestEnum.Test4, "Art")]
        public void ToDisplayTest(Enum value, string expected)
        {
            // Act
            var result = value.ToDisplay();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
