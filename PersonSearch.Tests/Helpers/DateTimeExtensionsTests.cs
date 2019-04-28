using NUnit.Framework;
using System;
using PersonSearch.Helpers;
using System.Collections.Generic;

namespace PersonSearch.Tests.Services
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        private static IList<TestCaseData> _toAgeTestCases = new List<TestCaseData> {
            new TestCaseData(new DateTime(2017, 05, 02), new DateTime(2019, 05, 01), 1)
                .SetName("ToDifference_Second comes after first"),
            new TestCaseData(new DateTime(2019, 05, 01), new DateTime(2017, 05, 02), 1)
                .SetName("ToDifference_First comes after second"),
            new TestCaseData(new DateTime(2017, 05, 02), new DateTime(2017, 05, 02), 0)
                .SetName("ToDifference_Same date"),
            new TestCaseData(new DateTime(2017, 05, 01), new DateTime(2019, 05, 02), 2)
                .SetName("ToDifference_Edge of year case"),
            new TestCaseData(new DateTime(1500, 01, 01), new DateTime(2001, 12, 31), 501)
                .SetName("ToDifference_Bigger difference"),
            new TestCaseData(new DateTime(2017, 04, 26), new DateTime(2019, 05, 02), 2)
                .SetName("ToDifference_Month before but day after case"),
        };

        [TestCaseSource(nameof(_toAgeTestCases))]
        public void ToDifferenceTest(DateTime date, DateTime compareDate, int expected)
        {
            // Act
            var result = date.ToYearDifference(compareDate);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
