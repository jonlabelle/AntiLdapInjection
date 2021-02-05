// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpperMiddle.cs" company="Microsoft Corporation">
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
//   Provides safe character positions for the upper middle section of the UTF code tables.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AntiLdapInjection
{
    using System.Collections;
    using System.Linq;

    /// <summary>
    /// Provides safe character positions for the upper middle section of the UTF code tables.
    /// </summary>
    internal static class UpperMiddle
    {
        /// <summary>
        /// Determines if the specified flag is set.
        /// </summary>
        /// <param name="flags">The value to check.</param>
        /// <param name="flagToCheck">The flag to check for.</param>
        /// <returns>true if the flag is set, otherwise false.</returns>
        internal static bool IsFlagSet(UpperMidCodeCharts flags, UpperMidCodeCharts flagToCheck)
        {
            return (flags & flagToCheck) != 0;
        }

        /// <summary>
        /// Provides the safe characters for the Cyrillic Extended A code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CyrillicExtendedA()
        {
            return CodeChartHelper.GetRange(11744, 11775);
        }

        /// <summary>
        /// Provides the safe characters for the Cyrillic Extended A code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable SupplementalPunctuation()
        {
            return CodeChartHelper.GetRange(11776, 11825);
        }

        /// <summary>
        /// Provides the safe characters for the CJK Radicals Supplement code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CjkRadicalsSupplement()
        {
            return CodeChartHelper.GetRange(11904, 12019, (int i) => i == 11930);
        }

        /// <summary>
        /// Provides the safe characters for the Kangxi Radicals code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable KangxiRadicals()
        {
            return CodeChartHelper.GetRange(12032, 12245);
        }

        /// <summary>
        /// Provides the safe characters for the Ideographic Description Characters code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable IdeographicDescriptionCharacters()
        {
            return CodeChartHelper.GetRange(12272, 12283);
        }

        /// <summary>
        /// Provides the safe characters for the CJK Symbols and Punctuation code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CjkSymbolsAndPunctuation()
        {
            return CodeChartHelper.GetRange(12288, 12351);
        }

        /// <summary>
        /// Provides the safe characters for the Hiragana code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Hiragana()
        {
            return CodeChartHelper.GetRange(12353, 12447, (int i) => i == 12439 || i == 12440);
        }

        /// <summary>
        /// Provides the safe characters for the Hiragana code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Katakana()
        {
            return CodeChartHelper.GetRange(12448, 12543);
        }

        /// <summary>
        /// Provides the safe characters for the Bopomofo code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Bopomofo()
        {
            return CodeChartHelper.GetRange(12549, 12589);
        }

        /// <summary>
        /// Provides the safe characters for the Hangul Compatibility Jamo code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable HangulCompatibilityJamo()
        {
            return CodeChartHelper.GetRange(12593, 12686);
        }

        /// <summary>
        /// Provides the safe characters for the Kanbun code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Kanbun()
        {
            return CodeChartHelper.GetRange(12688, 12703);
        }

        /// <summary>
        /// Provides the safe characters for the Bopomofo Extended code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable BopomofoExtended()
        {
            return CodeChartHelper.GetRange(12704, 12727);
        }

        /// <summary>
        /// Provides the safe characters for the CJK Strokes code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CjkStrokes()
        {
            return CodeChartHelper.GetRange(12736, 12771);
        }

        /// <summary>
        /// Provides the safe characters for the Katakana Phonetic Extensions code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable KatakanaPhoneticExtensions()
        {
            return CodeChartHelper.GetRange(12784, 12799);
        }

        /// <summary>
        /// Provides the safe characters for the Enclosed CJK Letters and Months code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable EnclosedCjkLettersAndMonths()
        {
            return CodeChartHelper.GetRange(12800, 13054, (int i) => i == 12831);
        }

        /// <summary>
        /// Provides the safe characters for the CJK Compatibility code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CjkCompatibility()
        {
            return CodeChartHelper.GetRange(13056, 13311);
        }

        /// <summary>
        /// Provides the safe characters for the CJK Unified Ideographs Extension A code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CjkUnifiedIdeographsExtensionA()
        {
            return CodeChartHelper.GetRange(13312, 19893);
        }

        /// <summary>
        /// Provides the safe characters for the Yijing Hexagram Symbols code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable YijingHexagramSymbols()
        {
            return CodeChartHelper.GetRange(19904, 19967);
        }

        /// <summary>
        /// Provides the safe characters for the CJK Unified Ideographs code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CjkUnifiedIdeographs()
        {
            return CodeChartHelper.GetRange(19968, 40907);
        }

        /// <summary>
        /// Provides the safe characters for the Yi Syllables code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable YiSyllables()
        {
            return CodeChartHelper.GetRange(40960, 42124);
        }

        /// <summary>
        /// Provides the safe characters for the Yi Radicals code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable YiRadicals()
        {
            return CodeChartHelper.GetRange(42128, 42182);
        }

        /// <summary>
        /// Provides the safe characters for the Lisu code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Lisu()
        {
            return CodeChartHelper.GetRange(42192, 42239);
        }

        /// <summary>
        /// Provides the safe characters for the Vai code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Vai()
        {
            return CodeChartHelper.GetRange(42240, 42539);
        }

        /// <summary>
        /// Provides the safe characters for the Cyrillic Extended B code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CyrillicExtendedB()
        {
            return CodeChartHelper.GetRange(42560, 42647,
                (int i) => i == 42592 || i == 42593 || (i >= 42612 && i <= 42619));
        }

        /// <summary>
        /// Provides the safe characters for the Bamum code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Bamum()
        {
            return CodeChartHelper.GetRange(42656, 42743);
        }

        /// <summary>
        /// Provides the safe characters for the Modifier Tone Letters code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable ModifierToneLetters()
        {
            return CodeChartHelper.GetRange(42752, 42783);
        }

        /// <summary>
        /// Provides the safe characters for the Latin Extended D code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable LatinExtendedD()
        {
            return CodeChartHelper.GetRange(42784, 42892).Concat(CodeChartHelper.GetRange(43003, 43007));
        }

        /// <summary>
        /// Provides the safe characters for the Syloti Nagri code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable SylotiNagri()
        {
            return CodeChartHelper.GetRange(43008, 43051);
        }

        /// <summary>
        /// Provides the safe characters for the Common Indic Number Forms code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CommonIndicNumberForms()
        {
            return CodeChartHelper.GetRange(43056, 43065);
        }

        /// <summary>
        /// Provides the safe characters for the Phags-pa code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Phagspa()
        {
            return CodeChartHelper.GetRange(43072, 43127);
        }

        /// <summary>
        /// Provides the safe characters for the Saurashtra code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Saurashtra()
        {
            return CodeChartHelper.GetRange(43136, 43225, (int i) => i >= 43205 && i <= 43213);
        }
    }
}
