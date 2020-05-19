using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Axon.Core.Guards
{
    public static class Ensure
    {
        private static Regex _isGuidRegex = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        public static class Arguments
        {
            public static void ThrowIfInvalidDate(DateTime date, string name)
            {
                if (!IsValidDate(date)) throw new ArgumentException(name);
            }
            public static bool IsValidDate(DateTime date)
            {
                return date > DateTime.MinValue;
            }
            public static void ThrowIfNull(object argument, string name)
            {
                if (!IsNotNull(argument)) throw new ArgumentNullException(name);
            }

            public static bool IsNotNull(object argument)
            {
                return !(argument is null);
            }

            public static void ThrowIfNotValidGuid(string argument, string name)
            {
                ThrowIfStringEmpty(argument, name);

                if (!IsValidGuid(argument)) throw new ArgumentException(name);
            }

            public static bool IsValidGuid(string argument)
            {
#if DEBUG
                // In debug mode data can be inject manually in database with "invalid uuid" as identifier
                return !string.IsNullOrWhiteSpace(argument);
#else
                return IsNotNull(argument) && _isGuidRegex.IsMatch(argument);
#endif
            }

            public static void ThrowIfStringEmpty(string argument, string name)
            {
                if (IsStringEmpty(argument)) throw new ArgumentNullException(name);
            }

            public static bool IsStringEmpty(string argument)
            {
                return string.IsNullOrWhiteSpace(argument);
            }

            static bool invalid = false;

            public static bool IsValidEmail(string strIn)
            {
                invalid = false;
                if (string.IsNullOrEmpty(strIn))
                    return false;

                // Use IdnMapping class to convert Unicode domain names.
                try
                {
                    strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                          RegexOptions.None, TimeSpan.FromMilliseconds(200));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }

                if (invalid)
                    return false;

                // Return true if strIn is in valid email format.
                try
                {
                    return Regex.IsMatch(strIn,
                          @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                          RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }

            private static string DomainMapper(Match match)
            {
                // IdnMapping class with default property values.
                IdnMapping idn = new IdnMapping();

                string domainName = match.Groups[2].Value;
                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    invalid = true;
                }
                return match.Groups[1].Value + domainName;
            }


        }
    }
}
