using System.Globalization;

namespace RequestManager.Extensions
{
    public static class StringExtensions
    {
        public static bool Match(this string target, string pattern)
        {
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(target, pattern, CompareOptions.IgnoreCase) > -1;
        }
    }
}