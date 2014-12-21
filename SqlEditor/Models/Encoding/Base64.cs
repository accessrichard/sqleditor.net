namespace SqlEditor.Models.Encoding
{
    using System;
    using System.Text;

    /// <summary>
    /// Helper to encode using base 64.
    /// </summary>
    public class Base64
    {
        /// <summary>
        /// Encodes a string using base 64.
        /// </summary>
        /// <param name="str">The string to encode.</param>
        /// <returns>The encoded string.</returns>
        public static string Encode(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));   
        }

        /// <summary>
        /// Decodes a string using base 64.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <returns>The decoded string.</returns>
        public static string Decode(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
    }
}