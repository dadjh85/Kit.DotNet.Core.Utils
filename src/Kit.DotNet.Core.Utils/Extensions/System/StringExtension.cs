using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Kit.DotNet.Core.Utils.Extensions.System
{
    /// <summary>
    /// Utility extension for String objects.
    /// </summary>
    public static class StringExtension
    {
        #region "Static Methods"

        /// <summary>
        ///     Sets the first letter of the string to uppercase.
        /// </summary>
        /// <param name="s">String type object</param>
        /// <returns>String type object.</returns>
        public static string Capitalize(this string s)
        {
            ValidateString(s);
            return s.Substring(0, 1).ToUpper() + (s.Length > 1 ? s.Substring(1) : "");
        }

        /// <summary>
        ///     Sets the first letter of the string to lowercase.
        /// </summary>
        /// <param name="s">String type object.</param>
        /// <returns>String type object.</returns>
        public static string Uncapitalize(this string s)
        {
            ValidateString(s);
            return s.Substring(0, 1).ToLower() + (s.Length > 1 ? s.Substring(1) : "");
        }

        /// <summary>
        ///     Checks if a string is contained within another.
        /// </summary>
        /// <param name="s">String type object</param>
        /// <param name="search">String type object.</param>
        /// <param name="ignoreCase"><c>true</c> if search should ignore capitalization; <c>false</c> case sensitive search.</param>
        /// <returns><c>true</c> if s contains search string; <c>false</c> any othe case.</returns>
        public static bool Contains(this string s, string search, bool ignoreCase)
        {
            ValidateString(s, search);
            return !string.IsNullOrEmpty(s) && s.IndexOf(value: search, comparisonType: ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture) != -1;
        }

        /// <summary>
        ///     Checks if a normalized string is contained within another normalized string
        /// </summary>
        /// <param name="s">String type object.</param>
        /// <param name="search">String type object.</param>
        /// <param name="ignoreCase"><c>true</c> if search should ignore capitalization; <c>false</c> case sensitive search.</param>
        /// <returns><c>true</c> if s contains search string; <c>false</c> any othe case.</returns>
        public static bool ContainsNormalized(this string s, string search, bool ignoreCase = false)
        {
            ValidateString(s, search);
            return !string.IsNullOrEmpty(s) && s.ToNormalized().Contains(search: search.ToNormalized(), ignoreCase: ignoreCase);
        }

        /// <summary>
        ///     Normalizes a string to be used as HTML code. Substitues certain character fo HTMLENTITY equivalents.
        /// </summary>
        /// <param name="s">String type object.</param>
        /// <param name="useNonBreakingSpaces"><c>true</c> Substitues space characters for <c><![CDATA[&nbsp;]]></c>.</param>
        /// <returns>String type object.</returns>
        public static string NormalizeHtml(this string s, bool useNonBreakingSpaces = false)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;

            if (useNonBreakingSpaces)
                s = s.Replace(" ", "&nbsp;");

            s = s.Replace("-", "&ndash;");
            s = s.Replace("�", "&aacute;");
            s = s.Replace("�", "&Aacute;");
            s = s.Replace("�", "&eacute;");
            s = s.Replace("�", "&Eacute;");
            s = s.Replace("�", "&iacute;");
            s = s.Replace("�", "&Iacute;");
            s = s.Replace("�", "&oacute;");
            s = s.Replace("�", "&Oacute;");
            s = s.Replace("�", "&uacute;");
            s = s.Replace("�", "&Uacute;");
            s = s.Replace("�", "&ntilde;");
            s = s.Replace("�", "&Ntilde;");
            s = s.Replace("\r\n", "<br/>");
            s = s.Replace("\n", "<br/>");

            return s;
        }

        /// <summary>
        ///     Erases specified characters form source string.
        /// </summary>
        /// <param name="s">String type object.</param>
        /// <param name="parts">Char type array.</param>
        /// <returns><see cref="string" /> without characters in array.</returns>
        public static string RemoveText(this string s, params char[] parts)
            => s.RemoveText(parts.Select(p => p.ToString()).ToArray());

        /// <summary>
        ///     Erases specified strings form source string
        /// </summary>
        /// <param name="s">String type object.</param>
        /// <param name="parts">String type array.</param>
        /// <returns><see cref="string" /> without substrings in array.</returns>
        public static string RemoveText(this string s, params string[] parts)
        {
            return parts != null && parts.Length > 0
                     ? parts.Aggregate(seed: s, func: (current, replacePart) => replacePart.Length > 0 ? current.Replace(replacePart, string.Empty) : current)
                     : s;
        }

        /// <summary>
        ///     Splits source string in chunks of specific size
        /// </summary>
        /// <param name="s">String type object.</param>
        /// <param name="chunkSize">Int type object.</param>
        /// <returns>String type <see cref="IEnumerable{T}" /></returns>
        public static IEnumerable<string> SplitBlock(this string s, int chunkSize)
        {
            if (string.IsNullOrEmpty(s))
                return new List<string>();

            int totalBlocks = Convert.ToInt32(Math.Ceiling((double)s.Length / chunkSize));
            return Enumerable.Range(start: 0, count: totalBlocks)
                             .Select(i => s.Substring(startIndex: i * chunkSize,
                                                      length: Math.Min(chunkSize, s.Length - i * chunkSize)));
        }

       
        /// <summary>
        ///     Obtains the source enum�s field with the same name as String.
        /// </summary>
        /// <typeparam name="TEnum">TEnum type of object.</typeparam>
        /// <param name="s">String type object.</param>
        /// <returns><see cref="TEnum" /> type object.</returns>
        public static TEnum ToEnum<TEnum>(this string s) where TEnum : struct
        {
            if (string.IsNullOrEmpty(s))
                return default(TEnum);

            TEnum enumValue;
            return Enum.TryParse(value: s, result: out enumValue, ignoreCase: true) ? enumValue : default(TEnum);
        }

        /// <summary>
        ///     Normalizes source string.
        /// </summary>
        /// <param name="s">String type object.</param>
        /// <returns>Strint type object.</returns>
        public static string ToNormalized(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            // Reemplazamos las vocales acentuadas en base a las expresiones regulares definidas
            s = Regex.Replace(s, "[�|�|�|�|�]", "a", RegexOptions.None);
            s = Regex.Replace(s, "[�|�|�|�]", "e", RegexOptions.None);
            s = Regex.Replace(s, "[�|�|�|�]", "i", RegexOptions.None);
            s = Regex.Replace(s, "[�|�|�|�|�]", "o", RegexOptions.None);
            s = Regex.Replace(s, "[�|�|�|�]", "u", RegexOptions.None);

            s = Regex.Replace(s, "[�|�|�|�|�]", "A", RegexOptions.None);
            s = Regex.Replace(s, "[�|�|�|�]", "E", RegexOptions.None);
            s = Regex.Replace(s, "[�|�|�|�]", "I", RegexOptions.None);
            s = Regex.Replace(s, "[�|�|�|�|�]", "O", RegexOptions.None);
            s = Regex.Replace(s, "[�|�|�|�]", "U", RegexOptions.None);

            return s;
        }


        /// <summary>
        ///     Generates a list of type T form spliting the source string using the delimiter.
        /// </summary>
        /// <typeparam name="T">Type of return object.</typeparam>
        /// <param name="s">String type object.</param>
        /// <param name="delimiter">Strint type object.</param>
        /// <returns>T type <see cref="List{T}" />.</returns>
        public static List<T>? ToList<T>(this string s, string delimiter = ",")
            => !string.IsNullOrEmpty(s)
                   ? s.Split(separator: new[] { delimiter }, options: StringSplitOptions.None)
                      .Select(x => (T)Convert.ChangeType(x, typeof(T)))
                      .ToList()
                   : null;

        /// <summary>
        ///     Converts a string containing a number in hexadecimal format to the equivalent byte array.
        /// </summary>
        /// <param name="s">String containing a number in hexadecimal format.</param>
        /// <returns>Byte array equivalent to the hexadecimal value stored in the string.</returns>
        public static byte[] HexToByteArray(this string s)
        {
            ValidateString(s);
            int hexStringLength = s.Length / 2;
            byte[] bytes = new byte[hexStringLength];
            using (StringReader reader = new StringReader(s))
            {
                for (int i = 0; i < hexStringLength; i++)
                    bytes[i] = Convert.ToByte(new string(new[] { (char)reader.Read(), (char)reader.Read() }), 16);
            }

            return bytes;
        }

        /// <summary>
        ///     Converts a string to the byte array with the specified encoding for use in deserialization tasks.
        /// </summary>
        /// <param name="s">String to convert.</param>
        /// <param name="encoding">
        ///     Encoding used to read the characters from the input string. By default ASCII encoding will be used.
        /// </param>
        /// <returns>Byte array for use in deserialization tasks.</returns>
        public static byte[] ToByteArray(this string s, Encoding? encoding = null)
        {
            ValidateString(s);
            return (encoding ?? Encoding.UTF8).GetBytes(s);
        }

        /// <summary>
        ///     Converts a string to a StringContent object
        /// </summary>
        /// <param name="s">String to convert.</param>
        /// <param name="encoding">
        ///     Encoding used to read the characters from the input string. 
        ///     By default UTF-8 encoding will be used
        /// </param>
        /// <param name="mediaType">
        ///     The type of media to use for the content. By default the medium 'application/json' will be used
        /// </param>
        /// <returns>StringContent object based on inserted values.</returns>
        public static StringContent ToStringContent(this string s, Encoding? encoding = null, string mediaType = "application/json")
        {
            ValidateString(s);
            return new StringContent(s, encoding ?? Encoding.UTF8, mediaType);
        }

        /// <summary>
        /// Converts the specified string, which encodes the binary data as base-64 digits, to an array equivalent of
        /// unsigened 8-bit integers
        /// </summary>
        /// <param name="file">Strint to convert.</param>
        /// <returns>An array of 8-bit unsigned integers equivalent to file.</returns>
        public static byte[]? FromBase64ToByteArray(this string file)
            => !string.IsNullOrEmpty(file) ? Convert.FromBase64String(file) : null;

        #endregion

        #region Private Methods

        private static void ValidateString(params string[] s)
        {
            foreach (var item in s.ToList())
            {
                if (string.IsNullOrWhiteSpace(item))
                    throw new InvalidOperationException("the string can't be null or empty");
            }
        }

        #endregion
    }
}