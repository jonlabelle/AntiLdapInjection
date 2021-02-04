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
    /// Values for the lower-mid section of the UTF8 Unicode code tables, from U1000 to U1EFF.
    /// </summary>
    [Flags]
    internal enum LowerMidCodeCharts : long
    {
        /// <summary>
        /// No code charts from the lower-mid region of the Unicode tables are safe-listed.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The Myanmar code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1000.pdf</remarks>
        Myanmar = 0x1,

        /// <summary>
        /// The Georgian code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U10A0.pdf</remarks>
        Georgian = 0x2,

        /// <summary>
        /// The Hangul Jamo code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1100.pdf</remarks>
        HangulJamo = 0x4,

        /// <summary>
        /// The Ethiopic code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1200.pdf</remarks>
        Ethiopic = 0x8,

        /// <summary>
        /// The Ethiopic supplement code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1380.pdf</remarks>
        EthiopicSupplement = 0x10,

        /// <summary>
        /// The Cherokee code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U13A0.pdf</remarks>
        Cherokee = 0x20,

        /// <summary>
        /// The Unified Canadian Aboriginal Syllabics code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1400.pdf</remarks>
        UnifiedCanadianAboriginalSyllabics = 0x40,

        /// <summary>
        /// The Ogham code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1680.pdf</remarks>
        Ogham = 0x80,

        /// <summary>
        /// The Runic code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U16A0.pdf</remarks>
        Runic = 0x100,

        /// <summary>
        /// The Tagalog code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1700.pdf</remarks>
        Tagalog = 0x200,

        /// <summary>
        /// The Hanunoo code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1720.pdf</remarks>
        Hanunoo = 0x400,

        /// <summary>
        /// The Buhid code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1740.pdf</remarks>
        Buhid = 0x800,

        /// <summary>
        /// The Tagbanwa code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1760.pdf</remarks>
        Tagbanwa = 0x1000,

        /// <summary>
        /// The Khmer code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1780.pdf</remarks>
        Khmer = 0x2000,

        /// <summary>
        /// The Mongolian code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1800.pdf</remarks>
        Mongolian = 0x4000,

        /// <summary>
        /// The Unified Canadian Aboriginal Syllabics Extended code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U18B0.pdf</remarks>
        UnifiedCanadianAboriginalSyllabicsExtended = 0x8000,

        /// <summary>
        /// The Limbu code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1900.pdf</remarks>
        Limbu = 0x10000,

        /// <summary>
        /// The Tai Le code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1950.pdf</remarks>
        TaiLe = 0x20000,

        /// <summary>
        /// The New Tai Lue code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1980.pdf</remarks>
        NewTaiLue = 0x40000,

        /// <summary>
        /// The Khmer Symbols code table
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U19E0.pdf</remarks>
        KhmerSymbols = 0x80000,

        /// <summary>
        /// The Buginese code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1A00.pdf</remarks>
        Buginese = 0x100000,

        /// <summary>
        /// The Tai Tham code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1A20.pdf</remarks>
        TaiTham = 0x200000,

        /// <summary>
        /// The Balinese code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1B00.pdf</remarks>
        Balinese = 0x400000,

        /// <summary>
        /// The Sudanese code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1B80.pdf</remarks>
        Sudanese = 0x800000,

        /// <summary>
        /// The Lepcha code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1C00.pdf</remarks>
        Lepcha = 0x1000000,

        /// <summary>
        /// The Ol Chiki code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1C50.pdf</remarks>
        OlChiki = 0x2000000,

        /// <summary>
        /// The Vedic Extensions code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1CD0.pdf</remarks>
        VedicExtensions = 0x4000000,

        /// <summary>
        /// The Phonetic Extensions code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1D00.pdf</remarks>
        PhoneticExtensions = 0x8000000,

        /// <summary>
        /// The Phonetic Extensions Supplement code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1D80.pdf</remarks>
        PhoneticExtensionsSupplement = 0x10000000,

        /// <summary>
        /// The Combining Diacritical Marks Supplement code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1DC0.pdf</remarks>
        CombiningDiacriticalMarksSupplement = 0x20000000,

        /// <summary>
        /// The Latin Extended Additional code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1E00.pdf</remarks>
        LatinExtendedAdditional = 0x40000000
    }
}
