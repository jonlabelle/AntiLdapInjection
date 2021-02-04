// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeCharts.cs" company="Microsoft Corporation">
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
//   Enumerations for the various printable code tables within the Unicode UTF space.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AntiLdapInjection
{
    using System;

    /// <summary>
    /// Values for the upper middle section of the UTF8 Unicode code tables, from U2DE0 to UA8DF
    /// </summary>
    [Flags]
    internal enum UpperMidCodeCharts : long
    {
        /// <summary>
        /// No code charts from the lower region of the Unicode tables are safe-listed.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The Cyrillic Extended-A code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2DE0.pdf</remarks>
        CyrillicExtendedA = 0x1,

        /// <summary>
        /// The Supplemental Punctuation code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2E00.pdf</remarks>
        SupplementalPunctuation = 0x2,

        /// <summary>
        /// The CJK Radicials Supplement code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2E80.pdf</remarks>
        CjkRadicalsSupplement = 0x4,

        /// <summary>
        /// The Kangxi Radicials code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2F00.pdf</remarks>
        KangxiRadicals = 0x8,

        /// <summary>
        /// The Ideographic Description Characters code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2FF0.pdf</remarks>
        IdeographicDescriptionCharacters = 0x10,

        /// <summary>
        /// The CJK Symbols and Punctuation code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U3000.pdf</remarks>
        CjkSymbolsAndPunctuation = 0x20,

        /// <summary>
        /// The Hiragana code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U3040.pdf</remarks>
        Hiragana = 0x40,

        /// <summary>
        /// The Katakana code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U30A0.pdf</remarks>
        Katakana = 0x80,

        /// <summary>
        /// The Bopomofo code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U3100.pdf</remarks>
        Bopomofo = 0x100,

        /// <summary>
        /// The Hangul Compatbility Jamo code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U3130.pdf</remarks>
        HangulCompatibilityJamo = 0x200,

        /// <summary>
        /// The Kanbun code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U3190.pdf</remarks>
        Kanbun = 0x400,

        /// <summary>
        /// The Bopomofu Extended code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U31A0.pdf</remarks>
        BopomofoExtended = 0x800,

        /// <summary>
        /// The CJK Strokes code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U31C0.pdf</remarks>
        CjkStrokes = 0x1000,

        /// <summary>
        /// The Katakana Phonetic Extensoins code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U31F0.pdf</remarks>
        KatakanaPhoneticExtensions = 0x2000,

        /// <summary>
        /// The Enclosed CJK Letters and Months code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U3200.pdf</remarks>
        EnclosedCjkLettersAndMonths = 0x4000,

        /// <summary>
        /// The CJK Compatibility code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U3300.pdf</remarks>
        CjkCompatibility = 0x8000,

        /// <summary>
        /// The CJK Unified Ideographs Extension A code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U3400.pdf</remarks>
        CjkUnifiedIdeographsExtensionA = 0x10000,

        /// <summary>
        /// The Yijing Hexagram Symbols code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U4DC0.pdf</remarks>
        YijingHexagramSymbols = 0x20000,

        /// <summary>
        /// The CJK Unified Ideographs code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U4E00.pdf</remarks>
        CjkUnifiedIdeographs = 0x40000,

        /// <summary>
        /// The Yi Syllables code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA000.pdf</remarks>
        YiSyllables = 0x80000,

        /// <summary>
        /// The Yi Radicals code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA490.pdf</remarks>
        YiRadicals = 0x100000,

        /// <summary>
        /// The Lisu code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA4D0.pdf</remarks>
        Lisu = 0x200000,

        /// <summary>
        /// The Vai code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA500.pdf</remarks>
        Vai = 0x400000,

        /// <summary>
        /// The Cyrillic Extended-B code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA640.pdf</remarks>
        CyrillicExtendedB = 0x800000,

        /// <summary>
        /// The Bamum code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA6A0.pdf</remarks>
        Bamum = 0x1000000,

        /// <summary>
        /// The Modifier Tone Letters code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA700.pdf</remarks>
        ModifierToneLetters = 0x2000000,

        /// <summary>
        /// The Latin Extended-D code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA720.pdf</remarks>
        LatinExtendedD = 0x4000000,

        /// <summary>
        /// The Syloti Nagri code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA800.pdf</remarks>
        SylotiNagri = 0x8000000,

        /// <summary>
        /// The Common Indic Number Forms code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA830.pdf</remarks>
        CommonIndicNumberForms = 0x10000000,

        /// <summary>
        /// The Phags-pa code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA840.pdf</remarks>
        Phagspa = 0x20000000,

        /// <summary>
        /// The Saurashtra code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA880.pdf</remarks>
        Saurashtra = 0x40000000
    }
}
