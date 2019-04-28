namespace PersonSearch.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if string is null or only made up of white-space
        /// </summary>
        /// <param name="val">String</param>
        /// <returns></returns>
        public static bool IsNullOrBlank(this string val)
        {
            return val == null || val.Trim() == "";
        }
    }
}