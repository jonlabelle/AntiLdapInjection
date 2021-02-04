// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Encoder.cs" company="Microsoft Corporation">
//   Copyright (c) 2008, 2009, 2010 All Rights Reserved, Microsoft Corporation
//
//   This source is subject to the Microsoft Permissive License.
//   Please see the License.txt file for more information.
//   All other rights reserved.
//
//   THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//   KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//   IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//   PARTICULAR PURPOSE.
//
// </copyright>
// <summary>
//   Performs encoding of input strings to provide protection against
//   Cross-Site Scripting (XSS) attacks and LDAP injection attacks in
//   various contexts.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AntiLdapInjection
{
    using System;
    using System.Collections;
    using System.Text;

    /// <summary>
    /// Provides LDAP Encoding methods.
    /// </summary>
    /// <remarks></remarks>
    internal static class Encoder
    {
        /// <summary>
        /// The values to output for each character when filter encoding.
        /// </summary>
        private static readonly Lazy<char[][]> FilterCharacterValuesLazy = new Lazy<char[][]>(InitialiseFilterSafeList);

        /// <summary>
        /// The values to output for each character when DN encoding.
        /// </summary>
        private static readonly Lazy<char[][]> DistinguishedNameCharacterValuesLazy =
            new Lazy<char[][]>(InitialiseDistinguishedNameSafeList);

        /// <summary>
        /// Encodes the input string for use in LDAP filters.
        /// </summary>
        /// <param name="input">The string to encode.</param>
        /// <returns>An encoded version of the input string suitable for use in LDAP filters.</returns>
        internal static string FilterEncode(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var value = FilterCharacterValuesLazy.Value;
            var bytes = Encoding.UTF8.GetBytes(input.ToCharArray());

            var array = new char[bytes.Length * 3];
            var length = 0;

            foreach (var b in bytes)
            {
                if (value[b] != null)
                {
                    var array2 = value[b];
                    for (var j = 0; j < array2.Length; j++)
                    {
                        array[length++] = array2[j];
                    }
                }
                else
                {
                    array[length++] = (char) b;
                }
            }

            return new string(array, 0, length);
        }

        /// <summary>
        /// Encodes the input string for use in LDAP DNs.
        /// </summary>
        /// <param name="input">The string to encode.</param>
        /// <param name="useInitialCharacterRules">Value indicating whether the special case rules for encoding of spaces and octothorpes at the start of a string are used.</param>
        /// <param name="useFinalCharacterRule">Value indicating whether the special case for encoding of final character spaces is used.</param>
        /// <returns>An encoded version of the input string suitable for use in LDAP DNs.</returns>
        internal static string DistinguishedNameEncode(
            string input,
            bool useInitialCharacterRules,
            bool useFinalCharacterRule
        )
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var value = DistinguishedNameCharacterValuesLazy.Value;
            var bytes = Encoding.UTF8.GetBytes(input.ToCharArray());

            var array = new char[bytes.Length * 3];
            var length = 0;

            for (var i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];

                if (i == 0 && b == 32 && useInitialCharacterRules)
                {
                    array[length++] = '\\';
                    array[length++] = ' ';
                }
                else if (i == 0 && b == 35 && useInitialCharacterRules)
                {
                    array[length++] = '\\';
                    array[length++] = '#';
                }
                else if (i == bytes.Length - 1 && b == 32 && useFinalCharacterRule)
                {
                    array[length++] = '\\';
                    array[length++] = ' ';
                }
                else if (value[b] != null)
                {
                    var array2 = value[b];
                    for (var j = 0; j < array2.Length; j++)
                    {
                        array[length++] = array2[j];
                    }
                }
                else
                {
                    array[length++] = (char) b;
                }
            }

            return new string(array, 0, length);
        }

        /// <summary>
        /// Initializes the LDAP filter safe list.
        /// </summary>
        /// <returns>The LDAP filter safe list.</returns>
        private static char[][] InitialiseFilterSafeList()
        {
            var safeList = SafeList.Generate(255, SafeList.SlashThenHexValueGenerator);

            SafeList.PunchSafeList(ref safeList, FilterEncodingSafeList());

            return safeList;
        }

        /// <summary>
        /// Provides the safe characters for LDAP filter encoding.
        /// </summary>
        /// <returns>The safe characters for LDAP filter encoding.</returns>
        /// <remarks>See http://tools.ietf.org/html/rfc4515/</remarks>
        private static IEnumerable FilterEncodingSafeList()
        {
            for (var i = 32; i <= 126; i++)
            {
                if (i != 40 && i != 41 && i != 42 && i != 47 && i != 92)
                {
                    yield return i;
                }
            }
        }

        /// <summary>
        /// Initializes the LDAP DN safe lists.
        /// </summary>
        /// <returns>The DN safe list.</returns>
        private static char[][] InitialiseDistinguishedNameSafeList()
        {
            var safeList = SafeList.Generate(255, SafeList.HashThenHexValueGenerator);

            SafeList.PunchSafeList(ref safeList, DistinguishedNameSafeList());

            EscapeDistinguishedNameCharacter(ref safeList, ',');
            EscapeDistinguishedNameCharacter(ref safeList, '+');
            EscapeDistinguishedNameCharacter(ref safeList, '"');
            EscapeDistinguishedNameCharacter(ref safeList, '\\');
            EscapeDistinguishedNameCharacter(ref safeList, '<');
            EscapeDistinguishedNameCharacter(ref safeList, '>');
            EscapeDistinguishedNameCharacter(ref safeList, ';');

            return safeList;
        }

        /// <summary>
        /// Provides the safe characters for LDAP filter encoding.
        /// </summary>
        /// <returns>The safe characters for LDAP filter encoding.</returns>
        /// <remarks>See http://www.ietf.org/rfc/rfc2253.txt </remarks>
        private static IEnumerable DistinguishedNameSafeList()
        {
            for (var i = 32; i <= 126; i++)
            {
                if (
                    i != 44 && i != 43 &&
                    i != 34 && i != 92 &&
                    i != 60 && i != 62 &&
                    i != 38 && i != 33 &&
                    i != 124 && i != 61 &&
                    i != 45 && i != 39 &&
                    i != 59
                )
                {
                    yield return i;
                }
            }
        }

        /// <summary>
        /// Escapes a special DN character.
        /// </summary>
        /// <param name="safeList">The safe list to escape the character within.</param>
        /// <param name="c">The character to escape.</param>
        private static void EscapeDistinguishedNameCharacter(ref char[][] safeList, char c)
        {
            safeList[c] = new char[2] { '\\', c };
        }
    }
}
