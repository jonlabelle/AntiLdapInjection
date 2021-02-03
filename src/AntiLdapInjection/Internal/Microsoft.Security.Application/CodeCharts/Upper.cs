// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Upper.cs" company="Microsoft Corporation">
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
//   Provides safe character positions for the upper section of the UTF code tables.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AntiLdapInjection.Internal.Microsoft.Security.Application.CodeCharts
{
    using System.Collections;
    using System.Linq;

    /// <summary>
    /// Provides safe character positions for the upper section of the UTF code tables.
    /// </summary>
    internal static class Upper
    {
        /// <summary>
        /// Determines if the specified flag is set.
        /// </summary>
        /// <param name="flags">The value to check.</param>
        /// <param name="flagToCheck">The flag to check for.</param>
        /// <returns>true if the flag is set, otherwise false.</returns>
        public static bool IsFlagSet(UpperCodeCharts flags, UpperCodeCharts flagToCheck)
        {
            return (flags & flagToCheck) != 0;
        }

        /// <summary>
        /// Provides the safe characters for the Devanagari Extended code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable DevanagariExtended()
        {
            return CodeChartHelper.GetRange(43232, 43259);
        }

        /// <summary>
        /// Provides the safe characters for the Kayah Li code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable KayahLi()
        {
            return CodeChartHelper.GetRange(43264, 43311);
        }

        /// <summary>
        /// Provides the safe characters for the Rejang code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable Rejang()
        {
            return CodeChartHelper.GetRange(43312, 43347).Concat(new int[1]
            {
                43359
            });
        }

        /// <summary>
        /// Provides the safe characters for the Hangul Jamo Extended A code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable HangulJamoExtendedA()
        {
            return CodeChartHelper.GetRange(43360, 43388);
        }

        /// <summary>
        /// Provides the safe characters for the Javanese code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable Javanese()
        {
            return CodeChartHelper.GetRange(43392, 43487, (int i) => i == 43470 || (i >= 43482 && i <= 43485));
        }

        /// <summary>
        /// Provides the safe characters for the Cham code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable Cham()
        {
            return CodeChartHelper.GetRange(43520, 43615,
                (int i) => (i >= 43575 && i <= 43583) || i == 43598 || i == 43599 || i == 43610 || i == 43611);
        }

        /// <summary>
        /// Provides the safe characters for the Myanmar Extended A code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable MyanmarExtendedA()
        {
            return CodeChartHelper.GetRange(43616, 43643);
        }

        /// <summary>
        /// Provides the safe characters for the Myanmar Extended A code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable TaiViet()
        {
            return CodeChartHelper.GetRange(43648, 43714).Concat(CodeChartHelper.GetRange(43739, 43743));
        }

        /// <summary>
        /// Provides the safe characters for the Meetei Mayek code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable MeeteiMayek()
        {
            return CodeChartHelper.GetRange(43968, 44025, (int i) => i == 44014 || i == 44015);
        }

        /// <summary>
        /// Provides the safe characters for the Hangul Syllables code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable HangulSyllables()
        {
            return CodeChartHelper.GetRange(44032, 55203);
        }

        /// <summary>
        /// Provides the safe characters for the Hangul Jamo Extended B code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable HangulJamoExtendedB()
        {
            return CodeChartHelper.GetRange(55216, 55291,
                (int i) => i == 55239 || i == 55240 || i == 55241 || i == 55242);
        }

        /// <summary>
        /// Provides the safe characters for the CJK Compatibility Ideographs code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable CjkCompatibilityIdeographs()
        {
            return CodeChartHelper.GetRange(63744, 64217,
                (int i) => i == 64046 || i == 64047 || i == 64110 || i == 64111);
        }

        /// <summary>
        /// Provides the safe characters for the Alphabetic Presentation Forms code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable AlphabeticPresentationForms()
        {
            return CodeChartHelper.GetRange(64256, 64335,
                (int i) => (i >= 64263 && i <= 64274) || (i >= 64280 && i <= 64284) || i == 64311 || i == 64317 ||
                           i == 64319 || i == 64322 || i == 64325);
        }

        /// <summary>
        /// Provides the safe characters for the Arabic Presentation Forms A code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable ArabicPresentationFormsA()
        {
            return CodeChartHelper.GetRange(64336, 65021,
                (int i) => (i >= 64434 && i <= 64466) || (i >= 64832 && i <= 64847) || i == 64912 || i == 64913 ||
                           (i >= 64968 && i <= 65007));
        }

        /// <summary>
        /// Provides the safe characters for the Variation Selectors code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable VariationSelectors()
        {
            return CodeChartHelper.GetRange(65024, 65039);
        }

        /// <summary>
        /// Provides the safe characters for the Vertical Forms code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable VerticalForms()
        {
            return CodeChartHelper.GetRange(65040, 65049);
        }

        /// <summary>
        /// Provides the safe characters for the Combining Half Marks code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable CombiningHalfMarks()
        {
            return CodeChartHelper.GetRange(65056, 65062);
        }

        /// <summary>
        /// Provides the safe characters for the CJK Compatibility Forms code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable CjkCompatibilityForms()
        {
            return CodeChartHelper.GetRange(65072, 65103);
        }

        /// <summary>
        /// Provides the safe characters for the Small Form Variants code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable SmallFormVariants()
        {
            return CodeChartHelper.GetRange(65104, 65131, (int i) => i == 65107 || i == 65127);
        }

        /// <summary>
        /// Provides the safe characters for the Arabic Presentation Forms B code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable ArabicPresentationFormsB()
        {
            return CodeChartHelper.GetRange(65136, 65276, (int i) => i == 65141);
        }

        /// <summary>
        /// Provides the safe characters for the Half Width and Full Width Forms code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable HalfWidthAndFullWidthForms()
        {
            return CodeChartHelper.GetRange(65281, 65518,
                (int i) => i == 65471 || i == 65472 || i == 65473 || i == 65480 || i == 65481 || i == 65488 ||
                           i == 65489 || i == 65496 || i == 65497 || i == 65501 || i == 65502 || i == 65503 ||
                           i == 65511);
        }

        /// <summary>
        /// Provides the safe characters for the Specials code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        public static IEnumerable Specials()
        {
            return CodeChartHelper.GetRange(65529, 65533);
        }
    }
}
