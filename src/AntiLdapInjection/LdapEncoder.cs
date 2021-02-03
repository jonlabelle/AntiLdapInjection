// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LdapEncoder.cs" company="Microsoft Corporation">
//   Copyright (c) 2010 All Rights Reserved, Microsoft Corporation
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
//   Provides LDAP Encoding methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AntiLdapInjection
{
    using Internal.Microsoft.Security.Application;

    /// <summary>
    /// Performs encoding of input strings to provide protection against
    /// Cross-Site Scripting (XSS) attacks and LDAP injection attacks in
    /// various contexts.
    /// </summary>
    /// <remarks>
    /// This encoding library uses the Principle of Inclusions,
    /// sometimes referred to as "safe-listing" to provide protection
    /// against injection attacks.  With safe-listing protection,
    /// algorithms look for valid inputs and automatically treat
    /// everything outside that set as a potential attack.  This library
    /// can be used as a defense in depth approach with other mitigation
    /// techniques. It is suitable for applications with high security
    /// requirements.
    /// </remarks>
    public static class LdapEncoder
    {
        /// <summary>
        /// Encodes input strings for use as a value in Lightweight Directory Access Protocol (LDAP) filter queries.
        /// </summary>
        /// <param name="input">String to be encoded.</param>
        /// <returns>Encoded string for use as a value in LDAP filter queries.</returns>
        /// <remarks>
        ///     This method encodes all but known safe characters defined in the safe list.
        ///
        ///     <newpara />
        ///
        ///     RFC 4515 defines the format in which special characters need to be
        ///     escaped to be used inside a search filter. Special characters need to be
        ///     encoded in \XX format where XX is the hex representation of the character.
        ///
        ///     <newpara />
        ///
        ///     The following examples illustrate the use of the escaping mechanism.
        ///
        ///     <list type="table">
        ///         <item><term>Parens R Us (for all your parenthetical needs)</term><description>Parens R Us \28for all your parenthetical needs\29</description></item>
        ///         <item><term>*</term><description>\2A</description></item>
        ///         <item><term>C:\MyFile</term><description>C:\5CMyFile</description></item>
        ///         <item><term>NULLNULLNULLEOT (binary)</term><description>\00\00\00\04</description></item>
        ///         <item><term>Lučić</term><description>Lu\C4\8Di\C4\87</description></item>
        ///     </list>
        /// </remarks>
        public static string FilterEncode(string input)
        {
            return Encoder.FilterEncode(input);
        }

        /// <summary>
        /// Encodes input strings for use as a value in Lightweight Directory Access Protocol (LDAP) DNs.
        /// </summary>
        /// <param name="input">String to be encoded.</param>
        /// <returns>Encoded string for use as a value in LDAP DNs.</returns>
        /// <remarks>
        ///     This method encodes all but known safe characters defined in the safe list.
        ///
        ///     <newpara />
        ///
        ///     RFC 2253 defines the format in which special characters need to be
        ///     escaped to be used inside a search filter. Special characters need to be
        ///     encoded in #XX format where XX is the hex representation of the character or a
        ///     specific \ escape format.
        ///
        ///     <newpara />
        ///
        ///     The following examples illustrate the use of the escaping mechanism.
        ///
        ///     <list type="table">
        ///         <item><term>, + \ " \ &lt; &gt;</term><description>\, \+ \" \\ \&lt; \&gt;</description></item>
        ///         <item><term> hello</term><description>\ hello</description></item>
        ///         <item><term>hello </term><description>hello \ </description></item>
        ///         <item><term>#hello</term><description>\#hello</description></item>
        ///         <item><term>Lučić</term><description>Lu#C4#8Di#C4#87</description></item>
        ///     </list>
        /// </remarks>
        public static string DistinguishedNameEncode(string input)
        {
            return DistinguishedNameEncode(input: input, useInitialCharacterRules: true, useFinalCharacterRule: true);
        }

        /// <summary>
        /// Encodes input strings for use as a value in Lightweight Directory Access Protocol (LDAP) DNs.
        /// </summary>
        /// <param name="input">String to be encoded.</param>
        /// <param name="useInitialCharacterRules">Value indicating whether the special case rules for encoding of spaces and octothorpes at the start of a string are used.</param>
        /// <param name="useFinalCharacterRule">Value indicating whether the special case for encoding of final character spaces is used.</param>
        /// <returns>Encoded string for use as a value in LDAP DNs.</returns>\
        /// <remarks>
        ///     This method encodes all but known safe characters defined in the safe list.
        ///
        ///     <newpara />
        ///
        ///     RFC 2253 defines the format in which special characters need to be
        ///     escaped to be used inside a search filter. Special characters need to be
        ///     encoded in #XX format where XX is the hex representation of the character or a
        ///     specific \ escape format.
        ///
        ///     <newpara />
        ///
        ///     The following examples illustrate the use of the escaping mechanism.
        ///
        ///     <list type="table">
        ///         <item><term>, + \ " \ &lt; &gt;</term><description>\, \+ \" \\ \&lt; \&gt;</description></item>
        ///         <item><term> hello</term><description>\ hello</description></item>
        ///         <item><term>hello </term><description>hello\ </description></item>
        ///         <item><term>#hello</term><description>\#hello</description></item>
        ///         <item><term>Lučić</term><description>Lu#C4#8Di#C4#87</description></item>
        ///     </list>
        ///
        ///     If useInitialCharacterRules is set to false, then escaping of the initial space or octothorpe characters is not performed;
        ///
        ///     <list type="table">
        ///         <item><term>, + \ " \ &lt; &gt;</term><description>\, \+ \" \\ \&lt; \&gt;</description></item>
        ///         <item><term> hello</term><description> hello</description></item>
        ///         <item><term>hello </term><description>hello\ </description></item>
        ///         <item><term>#hello</term><description>#hello</description></item>
        ///         <item><term>Lučić</term><description>Lu#C4#8Di#C4#87</description></item>
        ///     </list>
        ///
        ///     If useFinalCharacterRule is set to false then escaping of a space at the end of a string is not performed;
        ///
        ///     <list type="table">
        ///         <item><term>, + \ " \ &lt; &gt;</term><description>\, \+ \" \\ \&lt; \&gt;</description></item>
        ///         <item><term> hello</term><description> hello</description></item>
        ///         <item><term>hello </term><description>hello </description></item>
        ///         <item><term>#hello</term><description>#hello</description></item>
        ///         <item><term>Lučić</term><description>Lu#C4#8Di#C4#87</description></item>
        ///     </list>
        /// </remarks>
        public static string DistinguishedNameEncode(
            string input,
            bool useInitialCharacterRules,
            bool useFinalCharacterRule
        )
        {
            return Encoder.DistinguishedNameEncode(input, useInitialCharacterRules, useFinalCharacterRule);
        }
    }
}
