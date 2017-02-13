using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace EdiInvoicing.Helper
{
    public static class StringExtension
    {
        /// =================================================================================================
        /// <summary>Values that represent RemainingCharacterTypes: Alpha, Numeric or AlphaNumeric</summary>
        /// <remarks>Bbrown, 4/3/2013.</remarks>
        /// =================================================================================================
        public enum RemainingCharacterTypes
        {
            AlphaOnly,
            NumericOnly,
            AlphaAndNumericOnly
        }



        /// =================================================================================================
        /// <summary>A string extension method that converts a value to a decimal.</summary>
        /// <remarks>Bbrown, 4/3/2013.</remarks>
        /// <param name="str">The value to act on.    </param>
        /// <returns>value as a decimal?</returns>
        /// =================================================================================================
        public static decimal? ToDecimal(this string value)
        {
            // you can throw an exception or return a default value here
            if (string.IsNullOrEmpty(value))
                return null;

            decimal d;

            var cleanValue = CleanNumberString(value);

            // you could throw an exception or return a default value on failure
            if (!decimal.TryParse(cleanValue, out d))
                return null;

            return d;
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that converts a value to a boolean. </summary>
        /// <remarks>   Bbrown, 6/2/2014. </remarks>
        /// <param name="value">    The value to act on. </param>
        /// <returns>   value as a Boolean? </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Boolean? ToBoolean(this string value)
        {
            Boolean? result = null;
            var tempBool = false;
            Byte tempByte = 9;


            if (string.IsNullOrWhiteSpace(value))
            {
                // return null if empty
                result = null;
            }
            else if (Boolean.TryParse(value, out tempBool))
            {
                //try to parse the string as a boolean first (as "true" or "false")
                result = tempBool;
            }
            else if (Byte.TryParse(value, out tempByte))
            {
                //That failed so try to convert it as a byte (as "1" or "0")
                switch (tempByte)
                {
                    case 0:
                        result = false;
                        break;

                    case 1:
                        result = true;
                        break;

                    default:
                        result = null;
                        break;
                }
            }

            return result;
        }


        /// <summary>
        /// Extension method designed to transform empty strings into null.
        /// </summary>
        /// <param name="value">The string to nullify or return.</param>
        /// <returns>Null if the string is already null or is empty or whitespace. Otherwise, the original string.</returns>
        public static string ToStringOrNull(this string value)
        {
            return String.IsNullOrWhiteSpace(value) ? null : value;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     A string extension method that converts a value to a
        ///     date time, but is very basic. Assums english culture
        ///     using DateTimeFormatInfo Class (see msdn). This
        ///     can be extended if greater flexibility is needed.
        /// </summary>
        /// <remarks>   Bbrown, 6/2/2014. </remarks>
        /// <param name="value">    The value to act on. </param>
        /// <returns>   value as a DateTime? </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static DateTime? ToDateTime(this string value)
        {
            //This can be expanded upon to include formats in the future
            // we can use either this one method or include an additional parameter
            // for format. Or even allow for Regex entry if we want as a
            // format layout validator

            // you can throw an exception or return a default value here
            if (string.IsNullOrEmpty(value))
                return null;

            DateTime d;

            // you could throw an exception or return a default value on failure
            if (!DateTime.TryParse(value, out d))
                return null;

            return d;
        }



        /// =================================================================================================
        /// <summary>A string extension method that converts a value to a integer.</summary>
        /// <remarks>Bbrown, 4/18/2014.</remarks>
        /// <param name="str">The value to act on.    </param>
        /// <returns>value as a int?</returns>
        /// =================================================================================================
        public static int? ToInteger(this string value)
        {
            // you can throw an exception or return a default value here
            if (string.IsNullOrEmpty(value))
                return null;

            int d;

            var cleanValue = CleanNumberString(value);

            // you could throw an exception or return a default value on failure
            if (!int.TryParse(value, out d))
                return null;

            return d;
        }



        public static List<string> ToList(this string value, string separator)
        {
            if (String.IsNullOrWhiteSpace(separator) || String.IsNullOrWhiteSpace(value))
                return null;

            String[] s = {separator};
            var list = value.Split(s, StringSplitOptions.RemoveEmptyEntries);

            return list.ToList();
        }



        /// =================================================================================================
        /// <summary>
        ///     A String extension method that formats a string to a phone if it is 10 digits.
        ///     Example: 1234567890 will become 132-456-7890
        ///     Any other string length or character combination will remain as is
        /// </summary>
        /// <remarks>Bbrown, 4/3/2013.</remarks>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have unsupported or illegal values.
        /// </exception>
        /// <param name="value">The value to act on.    </param>
        /// <returns>The formatted phone string.</returns>
        /// =================================================================================================
        public static string FormatPhone(this String value)
        {
            if (value == null)
                return null;

            var result = value;

            try
            {
                if (Regex.IsMatch(value, @"\A(\d{3})(\d{3})(\d{4})\z", RegexOptions.IgnoreCase))
                {
                    result = Regex.Replace(result, @"\A(\d{3})(\d{3})(\d{4})\z", "${1}-${2}-${3}",
                        RegexOptions.IgnoreCase);
                }
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }

            return result;
        }



        /// =================================================================================================
        /// <summary>
        ///     A String extension method that strips a string of all characters except the type
        ///     specified by the RemainingCharacterTypes.
        /// </summary>
        /// <remarks>Bbrown, 4/3/2013.</remarks>
        /// <param name="value">        The value to act on.    </param>
        /// <param name="remainingType">Type of the remaining characters.  </param>
        /// <returns>.</returns>
        /// =================================================================================================
        public static string StripString(this String value, RemainingCharacterTypes remainingType)
        {
            if (String.IsNullOrWhiteSpace(value))
                return value;

            var result = value;

            if (remainingType == RemainingCharacterTypes.AlphaOnly)
                result = Regex.Replace(result, "[^a-zA-Z]", "");
            else if (remainingType == RemainingCharacterTypes.NumericOnly)
                result = Regex.Replace(result, "[^0-9]", "");
            else
                result = Regex.Replace(result, "[^a-zA-Z0-9]", "");

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Shortcut for System.String.Format</summary>
        /// <example>
        ///     <para>Source: http://extensionmethod.net/csharp/string/format-string</para>
        ///     <code>
        ///                string greeting = "Hello {0}, my name is {1}, and I own you."
        ///                Console.WriteLine(greeting.Format("Adam", "Microsoft"))
        ///                 </code>
        /// </example>
        /// <remarks>BBrown, 1/26/2015.</remarks>
        /// <param name="format">        The format to act on.</param>
        /// <param name="arg">           The argument.</param>
        /// <param name="additionalArgs">
        ///     A variable-length parameters list containing additional
        ///     arguments.
        /// </param>
        /// <returns>The formatted value.</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string UseFormat(this string format, object arg, params object[] additionalArgs)
        {
            if (additionalArgs == null || additionalArgs.Length == 0)
            {
                return String.Format(format, arg);
            }
            return String.Format(format, new[] {arg}.Concat(additionalArgs).ToArray());
        }



        /// <summary>
        ///     Strip a string of the specified character.
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="char">character to remove from the string</param>
        /// <example>
        ///     string s = "abcdeabcde";
        ///     s = s.Strip('b');  //s becomes 'acdeacde;
        /// </example>
        /// <returns></returns>
        public static string Strip(this string s, char character)
        {
            s = s.Replace(character.ToString(), "");

            return s;
        }



        /// <summary>
        ///     Strip a string of the specified characters.
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="chars">list of characters to remove from the string</param>
        /// <example>
        ///     string s = "abcde";
        ///     s = s.Strip('a', 'd');  //s becomes 'bce;
        /// </example>
        /// <returns></returns>
        public static string Strip(this string s, params char[] chars)
        {
            foreach (var c in chars)
            {
                s = s.Replace(c.ToString(), "");
            }

            return s;
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Strip a string of the specified character.</summary>
        /// <example>
        ///     <para>Source: http://extensionmethod.net/csharp/string/strip</para>
        ///     <code>
        ///                 var s = "dogcatbirdfish";
        ///                 s = s.Strip("dog", "bird" ); // s becomes "catfish"
        ///                 </code>
        /// </example>
        /// <remarks>BBrown, 1/26/2015.</remarks>
        /// <param name="s">         the string to process.</param>
        /// <param name="subStrings">A variable-length parameters list containing sub strings.</param>
        /// <returns>A string.</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string Strip(this string s, params string[] subStrings)
        {
            foreach (var subString in subStrings)
            {
                s = s.Replace(subString, "");
            }

            return s;
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>returns default value if string is null or empty or white spaces string </summary>
        /// <example>
        ///     <para>Source: http://extensionmethod.net/csharp/string/defaultifempty</para>
        ///     <code>
        ///                string str = null;
        ///  str.DefaultIfEmpty("I'm nil") // return "I'm nil"
        /// 
        ///  string str1 = string.Empty;
        ///  str1.DefaultIfEmpty("I'm Empty") // return "I'm Empty!"
        /// 
        ///  string str1 = "     ";
        ///  str1.DefaultIfEmpty("I'm WhiteSpaces strnig!", true) // return "I'm WhiteSpaces strnig!"
        ///                 </code>
        /// </example>
        /// <remarks>BBrown, 1/27/2015.</remarks>
        /// <param name="str">                      The str to act on.</param>
        /// <param name="defaultValue">             The default value.</param>
        /// <param name="considerWhiteSpaceIsEmpty">    (Optional) true if consider white space is empty. Default: false</param>
        /// <returns>A string.</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string DefaultIfEmpty(this string str, string defaultValue, bool considerWhiteSpaceIsEmpty = false)
        {
            return ( considerWhiteSpaceIsEmpty ? string.IsNullOrWhiteSpace(str) : string.IsNullOrEmpty(str) )
                ? defaultValue
                : str;
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>returns default value if string is null or empty or white spaces string </summary>
        /// <example>
        ///     <para>Source: http://extensionmethod.net/csharp/string/defaultifempty</para>
        ///     <code>
        ///                string str = null;
        ///  str.DefaultIfEmpty("I'm nil") // return "I'm nil"
        /// 
        ///  string str1 = string.Empty;
        ///  str1.DefaultIfEmpty("I'm Empty") // return "I'm Empty!"
        /// 
        ///  string str1 = "     ";
        ///  str1.DefaultIfEmpty("I'm WhiteSpaces strnig!", true) // return "I'm WhiteSpaces strnig!"
        ///                 </code>
        /// </example>
        /// <remarks>BBrown, 1/27/2015.</remarks>
        /// <param name="str">                      The str to act on.</param>
        /// <param name="defaultValue">             The default value.</param>
        /// <param name="considerWhiteSpaceIsEmpty">
        ///     (Optional) true if consider white space is empty.
        ///     Default: false.
        /// </param>
        /// <returns>A string.</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static String DefaultValueIfEmpty(this String str, String defaultValue,
            bool considerWhiteSpaceIsEmpty = false)
        {
            return ( considerWhiteSpaceIsEmpty ? String.IsNullOrWhiteSpace(str) : String.IsNullOrEmpty(str) )
                ? defaultValue
                : str;
        }



        /// <summary>A string extension method that parses the given value insto a specific type
        ///          </summary>
        /// <remarks>BBrown, 4/8/2015.</remarks>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="value">The value to act on.</param>
        /// <returns>A T object.</returns>
        /// <example>
        /// Regular Parsing
        /// <code>
        ///     
        ///         int i = "123".Parse<int>();
        ///         int? inull = "123".Parse<int?>();
        ///         DateTime d = "01/12/2008".Parse<DateTime>();
        ///         DateTime? dn = "01/12/2008".Parse<DateTime?>();
        ///         
        ///  </code>
        /// Nullable values
        /// <code>
        /// 
        ///         string sample = null;
        ///         int? k = sample.Parse<int?>(); // returns null
        ///         int l = sample.Parse<int>();   // returns 0
        ///         DateTime dd = sample.Parse<DateTime>(); // returns 01/01/0001
        ///         DateTime? ddn = sample.Parse<DateTime?>(); // returns null
        ///
        /// </code>
        /// </example>
        public static T Parse<T>(this string value)
        {
            // Get default value for type so if string
            // is empty then we can return default value.
            var result = default( T );
            if (!string.IsNullOrEmpty(value))
            {
                // we are not going to handle exception here
                // if you need SafeParse then you should create
                // another method specially for that.
                var tc = TypeDescriptor.GetConverter(typeof (T));
                result = (T) tc.ConvertFrom(value);
            }
            return result;
        }


        /// <summary>An object extension method that convert this object into a string representation.
        ///          Created this to override ToString so it can try to handle null references and
        ///          allow for a default to be supplied in case it is null.</summary>
        ///
        /// <remarks>BBrown, 4/23/2015.</remarks>
        ///
        /// <param name="value">       The value to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        ///
        /// <returns>A String that represents this object. <see cref="defaultValue"/> will be returned for null
        ///          reference objects or an empty string is no <see cref="defaultValue"/> is supplied.</returns>

        public static String ToSafeString(this object value, String defaultValue)
        {

            var result = defaultValue ?? String.Empty;

            if (value == null || String.IsNullOrEmpty(value.ToString()))
                return result;

            return value.ToString();
            
        }



        /// <summary>An object extension method that convert this object into a string representation.
        ///          Created this to override ToString so it can try to handle null references and
        ///          allow for a default to be supplied in case it is null.</summary>
        ///
        /// <remarks>BBrown, 4/23/2015.</remarks>
        ///
        /// <param name="value">       The value to act on.</param>
        ///
        /// <returns>A String that represents this object. And empty string will be returned if object is null</returns>

        public static String ToSafeString(this object value)
        {
            return value.ToSafeString(String.Empty);
        }


        /// <summary>An object extension method that query if ToString 'value'
        ///          of object is numeric.</summary>
        ///
        /// <remarks>BBrown, 9/9/2015.</remarks>
        ///
        /// <param name="value">The value to act on.</param>
        ///
        /// <returns>true if numeric, false if not or NULL.</returns>

        public static bool IsNumeric(this object value)
        {
            Regex reNum = new Regex(@"^\d+$");
            bool isNumeric = reNum.Match(value.ToSafeString("")).Success;

            return isNumeric;
        }

        /// <summary> (This method is obsolete) a string extension method that queries if a null is empty. </summary>
        /// <param name="value"> The string to act on. </param>
        /// <returns> true if the null is empty, false if not. </returns>
        [Obsolete("Use IsNullOrEmpty extension.")]
        public static bool IsNullEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary> (This method is obsolete) a string extension method that query if 'value' is null whitespace. </summary>
        /// <param name="value"> The string to act on. </param>
        /// <returns> true if null whitespace, false if not. </returns>
        [Obsolete("Use IsNullOrWhiteSpace extension.")]
        public static bool IsNullWhitespace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary> A string extension method that queries if a null or is empty. </summary>
        /// <param name="value"> The string to act on. </param>
        /// <returns> true if the null or is empty, false if not. </returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary> A string extension method that query if 'value' is null or white space. </summary>
        /// <param name="value"> The string to act on. </param>
        /// <returns> true if null or white space, false if not. </returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// =================================================================================================
        /// <summary>Removes commas, dollar signs, percent symbol, spaces, and dashes from numeric strings.</summary>
        /// <param name="numberString">String containing number to cleanse.</param>
        /// <returns>Perged string or empty string</returns>
        /// =================================================================================================
        private static string CleanNumberString(string numberString)
        {
            if (String.IsNullOrWhiteSpace(numberString))
                return "";

            var tempStr = Regex.Replace(numberString, "[,$% ]*", "");

            if (tempStr.Contains('-'))
                tempStr = '-' + Regex.Replace(tempStr, "[-]*", "");

            return tempStr;
        }

        ///=================================================================================================
        /// <summary>Truncates a string to a certain lenght if longer</summary>
        ///
        /// <remarks>Bbrown, 3/31/2012.</remarks>
        ///
        /// <param name="source">Source string.    </param>
        /// <param name="length">The length to trim to.    </param>
        ///
        /// <returns>.</returns>
        ///=================================================================================================
        public static string Truncate(string source, int length)
        {
            if (source != null && source.Length > length)
                source = source.Substring(0, length);

            return source;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Helper method to query if 'value' is boolean of "true" or "false" 
        ///             using the TryParse function. (disregarding case) </summary>
        ///
        /// <remarks>   Bbrown, 6/27/2014. </remarks>
        ///
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   true if boolean text, false if not. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static bool IsBooleanText(string value)
        {
            bool result = false;

            if (String.IsNullOrWhiteSpace(value))
                return result;

            bool tempValue = false;

            result = bool.TryParse(value, out tempValue);

            return result;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Helper method to query if 'value' is an integer value in the string
        ///             using the TryParse function. </summary>
        ///
        /// <remarks>   Bbrown, 6/27/2014. </remarks>
        ///
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   true if integer text, false if not. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static bool IsIntegerText(string value)
        {
            bool result = false;

            if (String.IsNullOrWhiteSpace(value))
                return result;

            int tempValue = -1;

            result = int.TryParse(value, out tempValue);

            return result;

        }
    }
}