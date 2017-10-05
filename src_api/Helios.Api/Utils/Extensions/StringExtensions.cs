using System;
using System.Text.RegularExpressions;

namespace Helios.Api.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string ClearifyString(this string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            var returnValue = value.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);
            returnValue = Regex.Replace(returnValue, @"\s+", "");

            return returnValue;
        }
    }
}
