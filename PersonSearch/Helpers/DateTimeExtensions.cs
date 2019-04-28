using System;

namespace PersonSearch.Helpers
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get the different in years between this date and the compare date
        /// </summary>
        /// <param name="date">DateTime to convert</param>
        /// <param name="compareDate">DateTime to compare</param>
        /// <returns>Difference in whole years</returns>
        public static int ToYearDifference(this DateTime date, DateTime compareDate)
        {
            // Swap the dates if needed so we only need to do math based on one scenario (compareDate is after date)
            var tempDate = date;
            if (date > compareDate) {
                date = compareDate;
                compareDate = tempDate;
            }

            var diff = compareDate.Year - date.Year;
            if (compareDate.Month < date.Month || compareDate.Month == date.Month && compareDate.Day < date.Day) {
                diff--;
            }
            return diff;
        }
    }
}